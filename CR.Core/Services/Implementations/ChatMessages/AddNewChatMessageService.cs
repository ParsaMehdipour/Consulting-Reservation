using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.DTOs.ResultDTOs.ChatMessages;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ChatUserMessages;
using CR.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CR.Core.Services.Implementations.ChatMessages
{
    public class AddNewChatMessageService : IAddNewChatMessageService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public AddNewChatMessageService(ApplicationContext context,
            IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto<ResultAddChatMessageDto> Execute(RequestAddNewChatMessageDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                string userId = "";
                bool onlineStatus = false;
                int notReadCount = 0;

                if (string.IsNullOrWhiteSpace(request.message) && string.IsNullOrWhiteSpace(request.filePath))
                {
                    return new ResultDto<ResultAddChatMessageDto>()
                    {
                        IsSuccess = false,
                        Message = "لطفا مطلبی جهت ارسال وارد کنید",
                        Data = null
                    };
                }

                var chatUser = _context.ChatUsers
                    .Include(_ => _.Appointment)
                    .ThenInclude(_ => _.TimeOfDay)
                    .Include(_ => _.ChatUserMessages)
                    .FirstOrDefault(_ => _.Id == request.chatUserId);

                if (request.messageFlag == MessageFlag.ExpertMessage && chatUser!.Appointment.TimeOfDay.StartTime <= DateTime.Now && (chatUser.ChatStatus != ChatStatus.Closed && chatUser.ChatStatus != ChatStatus.Ended))
                {
                    chatUser.ChatStatus = ChatStatus.Started;
                }

                if (chatUser.Appointment.TimeOfDay.FinishTime < DateTime.Now)
                {
                    return new ResultDto<ResultAddChatMessageDto>()
                    {
                        IsSuccess = false,
                        Message = string.Empty,
                        Data = null
                    };
                }

                var chatMessage = new ChatUserMessage()
                {
                    ChatUserId = request.chatUserId,
                    ChatUser = _context.ChatUsers.Find(request.chatUserId),
                    MessageFlag = request.messageFlag,
                    IsRead = false
                };

                if (!string.IsNullOrWhiteSpace(request.message))
                {
                    if (chatUser.ChatUserMessages != null && chatUser.ChatUserMessages.Count > 0)
                    {
                        if (request.message == "مشاور وارد صفحه چت شد")
                        {
                            if (chatUser.ChatUserMessages.Last().Message == request.message)
                            {
                                return new ResultDto<ResultAddChatMessageDto>()
                                {
                                    Data = null,
                                    IsSuccess = false,
                                    Message = string.Empty
                                };
                            }
                        }

                        if (request.message == "کاربر وارد صفحه چت شد")
                        {
                            if (chatUser.ChatUserMessages.Last().Message == request.message)
                            {
                                return new ResultDto<ResultAddChatMessageDto>()
                                {
                                    Data = null,
                                    IsSuccess = false,
                                    Message = string.Empty
                                };
                            }
                        }
                    }

                    chatMessage.Message = request.message;
                }

                if (!string.IsNullOrWhiteSpace(request.filePath))
                {
                    chatMessage.File = request.filePath;
                }

                _context.ChatUserMessages.Add(chatMessage);

                _context.SaveChanges();

                if (request.messageFlag == MessageFlag.ConsumerMessage)
                {
                    userId = _context.ChatUsers.Include(_ => _.ExpertInformation)
                        .FirstOrDefault(_ => _.Id == request.chatUserId)?.ExpertInformation.ExpertId.ToString();

                    onlineStatus = _context.Users.Find(Convert.ToInt64(userId)).OnlineFlag;

                    notReadCount = _context.ChatUserMessages.Count(_ => _.IsRead == false && _.MessageFlag == MessageFlag.ConsumerMessage && _.ChatUserId == request.chatUserId);
                }
                else
                {
                    userId = _context.ChatUsers.Include(_ => _.Consumer)
                        .FirstOrDefault(_ => _.Id == request.chatUserId)?.Consumer.Id.ToString();

                    onlineStatus = _context.Users.Find(Convert.ToInt64(userId)).OnlineFlag;

                    notReadCount = _context.ChatUserMessages.Count(_ => _.IsRead == false && _.MessageFlag == MessageFlag.ExpertMessage && _.ChatUserId == request.chatUserId);

                }

                transaction.Commit();

                return new ResultDto<ResultAddChatMessageDto>()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Data = new ResultAddChatMessageDto()
                    {
                        userId = userId,
                        messageHour = $"{chatMessage.CreateDate.Minute} : {chatMessage.CreateDate.Hour}",
                        onlineStatus = onlineStatus,
                        NotReadCount = notReadCount
                    }
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto<ResultAddChatMessageDto>()
                {
                    Message = "پیام شما با موفقیت ارسال نشد!!",
                    IsSuccess = false,
                    Data = null
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
