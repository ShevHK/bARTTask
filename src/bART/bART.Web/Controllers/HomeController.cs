using bART.Creations;
using bART.Creations.HelperClasses;
using bART.Creations.Requests;
using bART.Creations.Response;
using bART.Model.Entities;
using bART.Model.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bART.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IRepository<incidents, string> _repositoryInc;
        private readonly IRepository<contacts, string> _repositoryCont;
        private readonly IRepository<accounts, string> _repositoryAcc;

        public HomeController(IRepository<incidents, string> repositoryInc, IRepository<contacts, string> repositoryCont, IRepository<accounts, string> repositoryAcc)
        {
            _repositoryInc = repositoryInc;
            _repositoryCont = repositoryCont;
            _repositoryAcc = repositoryAcc;
        }

        /// <summary>
        /// Get all incidents with account and contacts
        /// </summary>
        /// <response code="200">Response</response>
        /// <response code="400">Wrong request</response>
        /// 
        [ProducesResponseType(typeof(ApiResponse<GetAllResponse>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [HttpPost("get-all")]
        public async Task<ApiResponse<GetAllResponse>> GetAllTables()
        {

            try
            {
                var incidents = await _repositoryInc.GetItems();
                var accounts = await _repositoryAcc.GetItems();
                var contacts = await _repositoryCont.GetItems();

                var result = new ApiResponse<GetAllResponse>()
                {
                    Data = new GetAllResponse
                    {
                        incidents = (List<incidents>)incidents,
                        accounts = (List<accounts>)accounts,
                        contacts = (List<contacts>)contacts
                    },
                    Error = false
                };
                return result;

            }
            catch (Exception ex)
            {
                var result = new ApiResponse<GetAllResponse>()
                {
                    Result = ApiResponse<GetAllResponse>.f,
                    Data = null,
                    Error = true,
                    ErrorDescription = ex.Message
                };
                return result;
            }
        }

        /// <summary>
        /// Create incident
        /// </summary>
        /// <param name="createIncidentsRequest">Data</param>
        /// <response code="200">Response</response>
        /// <response code="400">Wrong pin-code</response>
        /// 
        [ProducesResponseType(typeof(ApiResponse<object>), 200)]
        [ProducesResponseType(typeof(ApiResponse<object>), 400)]
        [HttpPost("create-incidents")]
        public async Task<ApiResponse<object>> CreateIncidents(CreateIncidentsRequest createIncidentsRequest)
        {
            try
            {
                var incedentsList = new List<incidents>();
                var accountsList = new List<accounts>();
                var contactsList = new List<contacts>();
                


                foreach (var incedent in createIncidentsRequest.incidents)
                {
                    if (!incedent.accounts.Any())
                    {
                        var res = new ApiResponse<object>()
                        {
                            Data = null,
                            Error = true,
                            ErrorDescription = "incident must have any account"
                        };
                        return res;
                    }
                    
                    incedent.incident.name ??= await GetRandomName();

                    var inc = await _repositoryInc.GetItemByKey(incedent.incident.name);
                    if (inc != null)
                    {
                        var res = new ApiResponse<object>()
                        {
                            Data = null,
                            Error = true,
                            ErrorDescription = "incedent: \"" + incedent.incident.name + "\" is already exist"
                        };
                        return res;
                    }

                    incedentsList.Add(incedent.incident);


                    foreach (var account in incedent.accounts)
                    {
                        if (!account.contacts.Any())
                        {
                            var res = new ApiResponse<object>()
                            {
                                Data = null,
                                Error = true,
                                ErrorDescription = "account must have any contact"
                            };
                            return res;
                        }

                        var acc = await _repositoryAcc.GetItemByKey(account.name);
                        if (acc != null)
                        {
                            var res = new ApiResponse<object>()
                            {
                                Data = null,
                                Error = true,
                                ErrorDescription = "account: \"" + account.name + "\" is already exist"
                            };
                            return res;
                        }

                        var newAccount = new accounts()
                        {
                            name = account.name,
                            incidents_name = incedent.incident.name
                        };

                        accountsList.Add(newAccount);

                        foreach (var contact in account.contacts)
                        {
                            var cont = await _repositoryCont.GetItemByKey(contact.email);
                            if (cont != null)
                            {
                                var res = new ApiResponse<object>()
                                {
                                    Data = null,
                                    Error = true,
                                    ErrorDescription = "contacts: \"" + contact.email + "\" is already exist"
                                };
                                return res;
                            }

                            var newContact = new contacts()
                            {
                                email = contact.email,
                                first_name = contact.first_name,
                                Last_name = contact.last_name,
                                phone = contact.phone,
                                account_name = account.name
                            };
                            contactsList.Add(newContact);
                        }
                    }
                }

                await _repositoryInc.AddItems(incedentsList);
                await _repositoryAcc.AddItems(accountsList);
                await _repositoryCont.AddItems(contactsList);

                var result = new ApiResponse<object>()
                { Error = false };
                return result;

            }
            catch (Exception ex)
            {
                var result = new ApiResponse<object>()
                {
                    Data = null,
                    Error = true,
                    ErrorDescription = ex.Message
                };
                return result;
            }
        }

        private async Task<string> GetRandomName()
        {
            var flag = true;
            var name = "";
            while (flag)
            {
                name = NameGenerartor.Name();
                var res = await _repositoryInc.GetItemByKey(name);
                if (res == null)
                    flag = false;
            }
            return name;
        }


    }
}
