using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HealthDesk.Service
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDoctorService, DoctorService>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<ITimeSlotService, TimeSlotService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            return services;
        }
    }
}
