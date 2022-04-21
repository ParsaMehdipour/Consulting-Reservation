using CR.Common.Convertor;
using CR.Common.DTOs;
using CR.Core.DTOs.RequestDTOs.ChatUser;
using CR.Core.Services.Interfaces.ChatUsers;
using CR.DataAccess.Context;
using CR.DataAccess.Entities.ChatUsers;
using System;

namespace CR.Core.Services.Implementations.ChatUsers
{
    public class AddNewChatUserService : IAddNewChatUserService
    {
        private readonly ApplicationContext _context;

        public AddNewChatUserService(ApplicationContext context)
        {
            _context = context;
        }

        public ResultDto Execute(RequestAddNewChatUserDto request)
        {
            using var transaction = _context.Database.BeginTransaction();

            try
            {
                var chatUser = new ChatUser()
                {
                    Consumer = _context.Users.Find(request.consumerId),
                    ConsumerId = request.consumerId,
                    ExpertInformation = _context.ExpertInformations.Find(request.expertInformationId),
                    ExpertInformationId = request.expertInformationId,
                    MessageType = request.messageType,
                    AppointmentDate = request.appointmentDate,
                    AppointmentDate_String = request.appointmentDate.ToShamsi(),
                    AppointmentId = request.appointmentId,
                    Appointment = _context.Appointments.Find(request.appointmentId)
                };

                _context.ChatUsers.Add(chatUser);

                _context.SaveChanges();

                transaction.Commit();

                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = String.Empty
                };
            }
            catch (Exception)
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "خطا از سمت سرور"
                };
            }
        }
    }
}
