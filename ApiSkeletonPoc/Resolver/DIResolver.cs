
using ApiSkeletonPoc.Core.Insfrastucture.ErrorHandler;
using ApiSkeletonPoc.Core.Interfaces;
using ApiSkeletonPoc.Core.Repositories;
using ApiSkeletonPoc.Core.Services;
using EmailService;
using Microsoft.Extensions.DependencyInjection;
using TextMagic.Service;

namespace ApiSkeletonPoc.Resolver
{
    internal static class DIResolver
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            //services.AddScoped<IBaseRepository<DAL.DataContracts.Contact>, BaseRepository<DAL.DataContracts.Contact>> ();
            //services.AddScoped<IBaseService<DAL.DataContracts.Contact>, BaseService<DAL.DataContracts.Contact>>();
            services.AddScoped<IBaseRepository<DAL.DataContracts.Contact>, BaseRepository<DAL.DataContracts.Contact>>();
            services.AddScoped<IBaseService<DAL.DataContracts.Contact>, BaseService<DAL.DataContracts.Contact>>();
            services.AddScoped<IContactService, ContactService>();

            services
                .AddScoped<IBaseRepository<DAL.DataContracts.Individual>,
                    BaseRepository<DAL.DataContracts.Individual>>();
            services
                .AddScoped<IBaseService<DAL.DataContracts.Individual>,
                    BaseService<DAL.DataContracts.Individual>>();
            services.AddScoped<IIndividualService, IndividualService>();

            // Organization DI starts
            services
                .AddScoped<IBaseRepository<DAL.DataContracts.Organization>,
                    BaseRepository<DAL.DataContracts.Organization>>();
            services
                .AddScoped<IBaseService<DAL.DataContracts.Organization>,
                    BaseService<DAL.DataContracts.Organization>>();
            services.AddScoped<IOrganizationService, OrganizationService>();

            //Organization DI ends

            // Social Media DI starts
            services
                .AddScoped<IBaseRepository<DAL.DataContracts.SocialMedia>,
                    BaseRepository<DAL.DataContracts.SocialMedia>>();
            services
                .AddScoped<IBaseService<DAL.DataContracts.SocialMedia>,
                    BaseService<DAL.DataContracts.SocialMedia>>();
            services.AddScoped<ISocial_MediaService, Social_MediaService>();

            //Social Media DI ends

            //Address DI starts
            services
                .AddScoped<IBaseRepository<DAL.DataContracts.Address>,
                    BaseRepository<DAL.DataContracts.Address>>();
            services
                .AddScoped<IBaseService<DAL.DataContracts.Address>,
                    BaseService<DAL.DataContracts.Address>>();
            services.AddScoped<IAddressService, AddressService>();
            //Address DI ends

            //Org_Address DI starts
            services
                .AddScoped<IBaseRepository<DAL.DataContracts.Address>,
                    BaseRepository<DAL.DataContracts.Address>>();
            services
                .AddScoped<IBaseService<DAL.DataContracts.Address>,
                    BaseService<DAL.DataContracts.Address>>();
            services.AddScoped<IAddressService, AddressService>();

            //Org_Address DI ends


            //Phone number DI starts
            services
                .AddScoped<IBaseRepository<DAL.DataContracts.Phone>,
                    BaseRepository<DAL.DataContracts.Phone>>();
            services
                .AddScoped<IBaseService<DAL.DataContracts.Phone>,
                    BaseService<DAL.DataContracts.Phone>>();
            services.AddScoped<IPhone_NumbersService, Phone_NumbersService>();
            //Phone number DI ends

            //Client DI starts
            services
                .AddScoped<IBaseRepository<DAL.DataContracts.Client>,
                    BaseRepository<DAL.DataContracts.Client>>();
            services
                .AddScoped<IBaseService<DAL.DataContracts.Client>,
                    BaseService<DAL.DataContracts.Client>>();
            services.AddScoped<IClientService, ClientService>();
            //Client DI ends

            //Title DI starts
            services
                .AddScoped<IBaseRepository<DAL.DataContracts.Title>,
                    BaseRepository<DAL.DataContracts.Title>>();
            services
                .AddScoped<IBaseService<DAL.DataContracts.Title>,
                    BaseService<DAL.DataContracts.Title>>();
            services.AddScoped<ITitleService, TitleService>();
            //Title DI ends

            //Employee DI starts
            services
                .AddScoped<IBaseRepository<DAL.DataContracts.Employee>,
                    BaseRepository<DAL.DataContracts.Employee>>();
            services
                .AddScoped<IBaseService<DAL.DataContracts.Employee>,
                    BaseService<DAL.DataContracts.Employee>>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            //Employee DI ends

            //Visit DI starts
            services
                .AddScoped<IBaseRepository<DAL.DataContracts.Visit>,
                    BaseRepository<DAL.DataContracts.Visit>>();
            services
                .AddScoped<IBaseService<DAL.DataContracts.Visit>,
                    BaseService<DAL.DataContracts.Visit>>();
            services.AddScoped<IVisitsService, VisitsService>();
            //Visit DI ends

            services.AddScoped<IBaseRepository<DAL.DataContracts.LogEntry>, BaseRepository<DAL.DataContracts.LogEntry>>();
            services.AddScoped<IBaseService<DAL.DataContracts.LogEntry>, BaseService<DAL.DataContracts.LogEntry>>();

            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IErrorHandler, ErrorHandler>();
            services.AddScoped<IBaseRepository<DAL.DataContracts.User>, BaseRepository<DAL.DataContracts.User>>();
            services.AddScoped<IBaseService<DAL.DataContracts.User>, BaseService<DAL.DataContracts.User>>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.UserRoleMapping>, BaseRepository<DAL.DataContracts.UserRoleMapping>>();
            services.AddScoped<IBaseService<DAL.DataContracts.UserRoleMapping>, BaseService<DAL.DataContracts.UserRoleMapping>>();
            services.AddScoped<IUserRoleMappingService, UserRoleMappingService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.ClientModuleMapping>, BaseRepository<DAL.DataContracts.ClientModuleMapping>>();
            services.AddScoped<IBaseService<DAL.DataContracts.ClientModuleMapping>, BaseService<DAL.DataContracts.ClientModuleMapping>>();
            services.AddScoped<IClientModuleMappingService, ClientModuleMappingService>();


            services.AddScoped<IBaseRepository<DAL.DataContracts.Module>, BaseRepository<DAL.DataContracts.Module>>();
            services.AddScoped<IBaseService<DAL.DataContracts.Module>, BaseService<DAL.DataContracts.Module>>();

            services.AddScoped<ITextMagicClient, TextMagic.Service.TextMagicClient>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.Message>, BaseRepository<DAL.DataContracts.Message>>();
            services.AddScoped<IBaseService<DAL.DataContracts.Message>, BaseService<DAL.DataContracts.Message>>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.CustomField>, BaseRepository<DAL.DataContracts.CustomField>>();
            services.AddScoped<IBaseService<DAL.DataContracts.CustomField>, BaseService<DAL.DataContracts.CustomField>>();
            services.AddScoped<ICustomFieldService, CustomFieldService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.CustomFieldValue>, BaseRepository<DAL.DataContracts.CustomFieldValue>>();
            services.AddScoped<IBaseService<DAL.DataContracts.CustomFieldValue>, BaseService<DAL.DataContracts.CustomFieldValue>>();
            services.AddScoped<ICustomFieldValueService, CustomFieldValueService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.AmazonProducts>, BaseRepository<DAL.DataContracts.AmazonProducts>>();
            services.AddScoped<IBaseService<DAL.DataContracts.AmazonProducts>, BaseService<DAL.DataContracts.AmazonProducts>>();
            services.AddScoped<IAmazonMwsService, AmazonMwsService>();
            services.AddScoped<IEmailService, EmailService.EmailService>();
            services.AddScoped<IImageService, ImageService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.SignatureDoc>, BaseRepository<DAL.DataContracts.SignatureDoc>>();
            services.AddScoped<IBaseService<DAL.DataContracts.SignatureDoc>, BaseService<DAL.DataContracts.SignatureDoc>>();
            services.AddScoped<ISignatureService, SignatureService>();
            services.AddScoped<IDocumentService, DocumentService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.AmazonMwsProductsReOrderSettings>, BaseRepository<DAL.DataContracts.AmazonMwsProductsReOrderSettings>>();
            services.AddScoped<IBaseService<DAL.DataContracts.AmazonMwsProductsReOrderSettings>, BaseService<DAL.DataContracts.AmazonMwsProductsReOrderSettings>>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.JobEstimatorClient>, BaseRepository<DAL.DataContracts.JobEstimatorClient>>();
            services.AddScoped<IBaseService<DAL.DataContracts.JobEstimatorClient>, BaseService<DAL.DataContracts.JobEstimatorClient>>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.JobEstimatorProductStyle>, BaseRepository<DAL.DataContracts.JobEstimatorProductStyle>>();
            services.AddScoped<IBaseService<DAL.DataContracts.JobEstimatorProductStyle>, BaseService<DAL.DataContracts.JobEstimatorProductStyle>>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.JobEstimatorProductType>, BaseRepository<DAL.DataContracts.JobEstimatorProductType>>();
            services.AddScoped<IBaseService<DAL.DataContracts.JobEstimatorProductType>, BaseService<DAL.DataContracts.JobEstimatorProductType>>();

            services.AddScoped<IJobEstimatorService, JobEstimatorService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.ContactNotes>, BaseRepository<DAL.DataContracts.ContactNotes>>();
            services.AddScoped<IBaseService<DAL.DataContracts.ContactNotes>, BaseService<DAL.DataContracts.ContactNotes>>();

            services.AddScoped<IContactNotesService, ContactNotesService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.Job>, BaseRepository<DAL.DataContracts.Job>>();
            services.AddScoped<IBaseService<DAL.DataContracts.Job>, BaseService<DAL.DataContracts.Job>>();

            services.AddScoped<IJobService, JobService>();


            services.AddScoped<IBaseRepository<DAL.DataContracts.JobNotes>, BaseRepository<DAL.DataContracts.JobNotes>>();
            services.AddScoped<IBaseService<DAL.DataContracts.JobNotes>, BaseService<DAL.DataContracts.JobNotes>>();

            services.AddScoped<IJobNotesService, JobNotesService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.Valve>, BaseRepository<DAL.DataContracts.Valve>>();
            services.AddScoped<IBaseService<DAL.DataContracts.Valve>, BaseService<DAL.DataContracts.Valve>>();

            services.AddScoped<IValveService, ValveService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.ValveEvent>, BaseRepository<DAL.DataContracts.ValveEvent>>();
            services.AddScoped<IBaseService<DAL.DataContracts.ValveEvent>, BaseService<DAL.DataContracts.ValveEvent>>();

            services.AddScoped<IValveEventService, ValveEventService>();

            services.AddScoped<IBaseRepository<DAL.DataContracts.ValveEventType>, BaseRepository<DAL.DataContracts.ValveEventType>>();
            services.AddScoped<IBaseService<DAL.DataContracts.ValveEventType>, BaseService<DAL.DataContracts.ValveEventType>>();
            services.AddScoped<IFileService, FileService>();
        }

    }
}
