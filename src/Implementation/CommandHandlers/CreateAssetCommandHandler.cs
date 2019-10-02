using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Assets.Implementation.Commands;
using Core.Database;
using Core.Database.Tables;
using MediatR;
using Newtonsoft.Json;

namespace Core.Assets.Implementation.CommandHandlers
{
    public class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Asset>
    {
        private IBeawreContext _beawreContext;
        private IMapper _mapper;

        public CreateAssetCommandHandler(IBeawreContext beawreContext, IMapper mapper)
        {
            _beawreContext = beawreContext;
            _mapper = mapper;
        }

        public Task<Asset> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Asset>(request);

            entity.Payload = JsonConvert.SerializeObject(request.PayloadData);

            _beawreContext.Assets.Add(entity);
            _beawreContext.SaveChanges();

            return Task.FromResult(entity);
        }
    }
}
