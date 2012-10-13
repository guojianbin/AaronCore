using System.Collections.Generic;
using Aaron.Core.Domain.Messages;

namespace Aaron.Core.Services.Messages
{
    public partial interface ICampaignService
    {
        /// <summary>
        /// Inserts a campaign
        /// </summary>
        /// <param name="campaign">Campaign</param>        
        void InsertCampaign(Campaign campaign);

        /// <summary>
        /// Updates a campaign
        /// </summary>
        /// <param name="campaign">Campaign</param>
        void UpdateCampaign(Campaign campaign);

        /// <summary>
        /// Deleted a queued email
        /// </summary>
        /// <param name="campaign">Campaign</param>
        void DeleteCampaign(Campaign campaign);

        /// <summary>
        /// Gets a campaign by identifier
        /// </summary>
        /// <param name="campaignId">Campaign identifier</param>
        /// <returns>Campaign</returns>
        Campaign GetCampaignById(int campaignId);

        /// <summary>
        /// Gets all campaigns
        /// </summary>
        /// <returns>Campaign collection</returns>
        IList<Campaign> GetAllCampaigns();
    }
}
