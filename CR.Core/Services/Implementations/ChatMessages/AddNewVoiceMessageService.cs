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
    public class AddNewVoiceMessageService : IAddNewVoiceMessageService
    {
        private readonly ApplicationContext _context;
        private readonly IImageUploaderService _imageUploaderService;

        public AddNewVoiceMessageService(ApplicationContext context
        , IImageUploaderService imageUploaderService)
        {
            _context = context;
            _imageUploaderService = imageUploaderService;
        }

        public ResultDto Execute(RequestAddNewVoiceMessageDto request)
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


                if (request.file != null)
                {
                    chatMessage.Audio = _imageUploaderService.Execute(new UploadImageDto()
                    {
                        File = request.file,
                        Folder = "ChatVoices"
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
            catch (Exception e)
            {
                var error = e;

                transaction.Rollback();

                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا!!"
                };
            }
        }
    }
}
