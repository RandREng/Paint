using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace RandREng.GraphClient
{
    public class GraphHelper
    {
        private static GraphServiceClient graphClient;
        private static string tenant;
        public static void Initialize(IAuthenticationProvider authProvider, string tenant)
        {
            graphClient = new GraphServiceClient(authProvider);
            GraphHelper.tenant = tenant;
        }

        public static async Task<User> GetMeAsync()
        {
            try
            {
                // GET /me
                return await graphClient.Me.Request().GetAsync();
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting signed-in user: {ex.Message}");
                return null;
            }
        }

        public static async Task<IEnumerable<Event>> GetEventsAsync()
        {
            try
            {
                // GET /me/events
                var resultPage = await graphClient.Me.Events.Request()
                    // Only return the fields used by the application
                    .Select("subject,organizer,start,end")
                    // Sort results by when they were created, newest first
                    .OrderBy("createdDateTime DESC")
                    .GetAsync();

                return resultPage.CurrentPage;
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting events: {ex.Message}");
                return null;
            }
        }

        public static async Task<IEnumerable<Group>> GetTeamsAsync()
        {
            try
            {
                // GET /me/events
                var resultPage = await graphClient.Me.JoinedTeams.Request()
                    .GetAsync();

                return resultPage.CurrentPage;
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting events: {ex.Message}");
                return null;
            }
        }

        public static async Task<Group> GetTeam()
        {
            var item = await graphClient.Me.JoinedTeams.Request().GetAsync();
            return item.FirstOrDefault();
        }

        public static async Task<Channel> AddChannel(Group team, string name)
        {
            Channel channel = new Channel();
            channel.DisplayName = name;
            //            team.Channels.Add(channel);
            channel = await graphClient.Groups[team.Id].Team.Channels.Request().AddAsync(channel);
            //            var t = await graphClient.Teams[team.Id].Channels[channel.Id].;

            return channel;

        }

        public static async Task<IEnumerable<Channel>> GetChannelsAsync(string id)
        {
            try
            {
                // GET /me/events
                var resultPage = await graphClient.Teams[id].Channels.Request()
                    // Only return the fields used by the application
                    //                    .Select("subject,organizer,start,end")
                    // Sort results by when they were created, newest first
                    //                    .OrderBy("createdDateTime DESC")
                    //                    .Select("displayname,tabs")
                    //                    .Expand("tabs")
                    .GetAsync();

                return resultPage.CurrentPage;
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting events: {ex.Message}");
                return null;
            }
        }

        public static async Task<IEnumerable<PlannerPlan>> GetPlansAsync(string id)
        {
            try
            {
                var results = await graphClient.Groups[id].Planner.Plans.Request()
                    //                    .Expand("plans")
                    .GetAsync();

                return results.CurrentPage;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting planners: {ex.Message}");
                return null;
            }

        }

        public static async Task<IEnumerable<TeamsTab>> GetTabsAsync(string teamId, string channelId)
        {
            try
            {
                var results = await graphClient.Teams[teamId].Channels[channelId].Tabs.Request()
                    .Expand("teamsApp")
                    .GetAsync();

                return results.CurrentPage;
            }
            catch (ServiceException ex)
            {
                Console.WriteLine($"Error getting tabs: {ex.Message}");
                return null;
            }
        }

        public static async Task<PlannerPlan> AddPlan(string teamId, string title)
        {
            try
            {
                var plan = new PlannerPlan();
                plan.Owner = teamId;
                plan.Title = title;

                var results = await graphClient.Groups[teamId].Planner.Plans.Request()
                    .AddAsync(plan);
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error add plan: {ex.Message}");
                return null;
            }
        }

        public static async Task<TeamsTab> AddPlannerTab(string teamId, string channelId, string title)
        {
            PlannerPlan plan = null;
            TeamsTab newTab = null;

            try
            {
                plan = new PlannerPlan();
                plan.Owner = teamId;
                plan.Title = title;

                plan = await graphClient.Planner.Plans.Request()
                    .AddAsync(plan);
            }

            catch (ServiceException ex)
            {
                Console.WriteLine($"Error add plan: {ex.Message}");
                plan = null;
            }

            if (plan != null)
            {
                TeamsPlannerTab plannerTab = new TeamsPlannerTab(title, plan.Id);
                newTab = await graphClient.Teams[teamId].Channels[channelId].Tabs.Request().AddAsync(plannerTab);
                //using (HttpClient httpClient = new HttpClient())
                //{
                //    TeamsPlannerTab plannerTab = new TeamsPlannerTab("Plan Gamma2", plan.Id);
                //    var resp = await httpClient.PostAsJson(
                //        $"https://graph.microsoft.com/beta/teams/{teamId}/channels/{channelId}/tabs",
                //        plannerTab,
                //        graphClient.AuthenticationProvider as IAuthenticationProvider2);
                //    newTab = await resp.Content.ReadAsJsonAsync<TeamsTab>();
                //}


            }

            return newTab;

        }

        internal class TeamsPlannerTab : TeamsTab
        {
            public TeamsPlannerTab(string name, string planId) : base()
            {
                Configuration = new TeamsTabConfiguration
                {
                    EntityId = planId,
                    ContentUrl = $"https://tasks.office.com/{tenant}/Home/PlannerFrame?page=7&planId={planId}&auth_pvr=Orgid&auth_upn={{upn}}&mkt={{locale}}",
                    WebsiteUrl = $"https://tasks.office.com/{tenant}/Home/PlanViews/{planId}",
                    RemoveUrl = $"https://tasks.office.com/{tenant}/Home/PlannerFrame?page=13&planId={planId}&auth_pvr=Orgid&auth_upn={{upn}}&mkt={{locale}}",
                    ODataType = null
                };
                DisplayName = name;
                ODataBind = "https://graph.microsoft.com/beta/appCatalogs/teamsApps/com.microsoft.teamspace.tab.planner";
                ODataType = null;
            }

        }

        internal class PlannerTab : Tab
        {
            public PlannerTab(string name, string planId) : base()
            {
                Configuration = new Configuration
                {
                    entityId = planId,
                    contentUrl = $"https://tasks.office.com/{tenant}/Home/PlannerFrame?page=7&planId={planId}&auth_pvr=Orgid&auth_upn={{upn}}&mkt={{locale}}",
                    websiteUrl = $"https://tasks.office.com/{tenant}/Home/PlanViews/{planId}",
                    removeUrl = $"https://tasks.office.com/{tenant}/Home/PlannerFrame?page=13&planId={planId}&auth_pvr=Orgid&auth_upn={{upn}}&mkt={{locale}}"
                };
                DisplayName = name;
                TeamsAppId = "https://graph.microsoft.com/beta/appCatalogs/teamsApps/com.microsoft.teamspace.tab.planner";
            }
        }


        internal class Tab
        {
            public Tab()
            {
                Configuration = new Configuration();
            }

            //
            // Summary:
            //     Gets or sets display name. Name of the tab.
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "displayName", Required = Required.Default)]
            public string DisplayName { get; set; }

            //
            // Summary:
            //     Gets or sets an associated existing app with a teams tab.
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "teamsApp@odata.bind", Required = Required.Default)]
            public string TeamsAppId { get; set; }

            //
            // Summary:
            //     Gets or sets web url. Deep link url of the tab instance. Read only.
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "webUrl", Required = Required.Default)]
            public string WebUrl { get; set; }

            //
            // Summary:
            //     Gets or sets configuration. Container for custom settings applied to a tab. The
            //     tab is considered configured only once this property is set.
            [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "configuration", Required = Required.Default)]
            public Configuration Configuration { get; set; }
        }


        internal class Configuration
        {
            public string entityId { get; set; }
            public string contentUrl { get; set; }
            public string websiteUrl { get; set; }
            public string removeUrl { get; set; }
        }
    }

}
