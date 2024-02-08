using FunctionWithTraceLog.Data;
using MediatR;

namespace FunctionWithTraceLog.Commands
{
    /// <summary>
    /// Command for retrieving an OrganisationService.
    /// Created By: Mark Lintell.
    /// Created On: 2021-11-12.
    /// Created For: NKAM-9726.
    /// </summary>
    /// <remarks>
    /// Modified By: Mark Lintell.
    /// Modified On: 2021-11-22.
    /// Purpose: NKAM-9726, changing from FunctionMonkey to MediatR.
    /// </remarks>
    public class FutureProspectCommand : IRequest
    {
        /// <summary>
        /// Gets or sets the data for the command.
        /// Created By: Mark Lintell.
        /// Created On: 2021-11-22.
        /// Created For: NKAM-9726.
        /// </summary>
        public BusinessPartnerFutureProspectData Data { get; set; }
    }
}
