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
        private IDatabaseContext _databaseContext;
        private IMapper _mapper;

        public CreateAssetCommandHandler(IDatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public Task<Asset> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Asset>(request);

            entity.Payload = JsonConvert.SerializeObject(request.PayloadData);

            _databaseContext.Assets.Add(entity);
            _databaseContext.SaveChanges();

            return Task.FromResult(entity);
        }
    }
}
