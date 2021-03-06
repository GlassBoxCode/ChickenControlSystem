﻿using Bootstrapping.Services.Contract.BLL.Interface;
using Bootstrapping.Services.Contract.Crosscutting.Dto.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Enum.Sequencing;
using Bootstrapping.Services.Contract.Crosscutting.Interface.Sequencing;
using Bootstrapping.Services.Contract.HAL.Interface;

namespace BLL.HardwareModules.Door.Commands
{
    public class OpenDoorCommand : IOpenDoorCommand
    {
        private readonly IAxisOperations _axisOperations;
        private readonly IValidateOperationService _validateOperationService;
        private readonly IDoor _door;
        private readonly ICeilingSensor _ceiling;

        public int GetRunCount()
        {
            return 3;
        }

        public RecoveryOptionsDto RecoveryOptions { get; }

        public OpenDoorCommand(IAxisOperations axisOperations, IValidateOperationService validateOperationService,
            IDoor door, ICeilingSensor ceiling)
        {
            _axisOperations = axisOperations;
            _validateOperationService = validateOperationService;
            _door = door;
            _ceiling = ceiling;

            RecoveryOptions = new RecoveryOptionsDto();
        }

        public SequenceResultEnum Run()
        {
            var result = _axisOperations
                .MoveAxisSearch(
                    _door,
                    _ceiling,
                    true
                );
            return _validateOperationService.GetSequenceResult(result);
        }

        public void HandleFail()
        {
            throw new System.NotImplementedException();
        }
    }
}