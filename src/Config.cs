using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Core.Assets.Implementation.Commands;
using Core.Assets.Implementation.Commands.Risks;
using Core.Assets.Implementation.Commands.Treatments;
using Core.Assets.Implementation.Commands.Vulnerabilities;
using Core.Assets.Implementation.Services;
using Core.Assets.Interfaces.Services;
using Core.Assets.Models;
using Core.Database.Models;
using Core.Database.Tables;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Assets
{
    public class Config
    {
        public static void InitializeServices(ref IServiceCollection services)
        {
            // Services
            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetEdgeService, AssetEdgeService>();
            services.AddScoped<IVulnerabilityService, VulnerabilityService>();
            services.AddScoped<ITreatmentService, TreatmentService>();
            services.AddScoped<IRiskService, RiskService>();

            // Queries
        }
    }

    public class AssetsProfile : Profile
    {
        public AssetsProfile()
        {
            CreateMap<CreateAssetCommand, Asset>();
            CreateMap<Asset, AssetModel>();

            CreateMap<CreateVulnerabilityCommand, Vulnerability>();

            CreateMap<CreateTreatmentCommand, Treatment>();
            CreateMap<Treatment, TreatmentModel>();

            CreateMap<CreateRiskCommand, Risk>();
        }
    }
}
