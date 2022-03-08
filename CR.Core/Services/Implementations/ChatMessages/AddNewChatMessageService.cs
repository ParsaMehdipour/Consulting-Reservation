using CR.Common.DTOs;
using CR.Core.DTOs.Images;
using CR.Core.DTOs.RequestDTOs.Chat;
using CR.Core.Services.Interfaces.ChatMessages;
using CR.Core.Services.Interfaces.Images;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ChatUserMessages;
using System;

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

        public ResultDto Execute(RequestAddNewChatMessageDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {

                var chatMessage = new ChatUserMessage()
                {
                    ChatUserId = request.chatUserId,
                    ChatUser = _context.ChatUsers.Find(request.chatUserId),
                    MessageFlag = request.messageFlag,
                };

                if (string.IsNullOrWhiteSpace(request.message) && request.file == null)
                {
                    return new ResultDto()
                    {
                        IsSuccess = false,
                        Message = "لطفا مطلبی جهت ارسال وارد کنید"
                    };
                }

                if (!string.IsNullOrWhiteSpace(request.message))
                {
                    chatMessage.Message = request.message;
                }

                if (request.file != null)
                {
                    chatMessage.File = _imageUploaderService.Execute(new UploadImageDto()
                    {
                        File = request.file,
                        Folder = "ChatImages"
                    });
                }

                _context.ChatUserMessages.Add(chatMessage);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = string.Empty
                };
            }
            catch (Exception)
            {
                transaction.Rollback();

                return new ResultDto()
                {
                    Message = "پیام شما با موفقیت ارسال نشد!!",
                    IsSuccess = false
                };
            }
            finally
            {
                transaction.Dispose();
            }
        }
    }
}
