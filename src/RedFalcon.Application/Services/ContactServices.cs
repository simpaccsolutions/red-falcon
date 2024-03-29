﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using RedFalcon.Application.DTOs;
using RedFalcon.Application.Interfaces.Data;
using RedFalcon.Application.Interfaces.Services;
using RedFalcon.Application.Interfaces.Validator;
using RedFalcon.Application.ResourceParameters;
using RedFalcon.Application.ResultModels;
using RedFalcon.Domain.Entities;

namespace RedFalcon.Application.Services
{
    public class ContactServices : IContactServices
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactServices> _logger;
        private readonly IContactValidator _validator;

        public ContactServices(IUnitOfWork unitofwork, IMapper mapper, ILogger<ContactServices> logger, IContactValidator validator)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ViewContactDTO?> CreateAsync(CreateContactDTO contact)
        {
            try
            {
                var record = _mapper.Map<Contact>(contact);
                record.CreatedBy = "1";
                record.DateCreated = DateTime.UtcNow;

                await _validator.ValidateData(record);

                _unitofwork.CreateTransaction();

                await _unitofwork.Contacts.CreateAsync(record);

                _unitofwork.Commit();

                return _mapper.Map<ViewContactDTO>(record);

            }
            catch (Exception ex)
            {
                _unitofwork.Rollback();
                _logger.LogError($@"{ex.Message}");
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int contactId)
        {
            try
            {
                var record = await _unitofwork.Contacts.GetByIdAsync(contactId);
                if (record == null)
                {
                    return false;
                }

                _unitofwork.CreateTransaction();
                await _unitofwork.Contacts.DeleteAsync(record.Id);
                _unitofwork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _unitofwork.Rollback();
                _logger.LogError($@"{ex.Message}");
            }

            return false;
        }

        public async Task<PaginatedList<ViewContactDTO>> GetAsync(ContactResourceParameters resourceParameters)
        {
            resourceParameters.SearchFields = new List<string> { "Firstname", "Lastname", "Email", "Phone" };
            var result = await _unitofwork.Contacts.GetAsync(resourceParameters).ConfigureAwait(false);

            var paginatedResult = new PaginatedList<ViewContactDTO>(
                _mapper.Map<IEnumerable<ViewContactDTO>>(result.contacts).ToList(),
                result.recordCount,
                resourceParameters.Page,
                resourceParameters.PageSize);

            return paginatedResult;
        }

        public async Task<ViewContactDTO?> GetByIdAsync(int contactId)
        {
            var record = await _unitofwork.Contacts.GetByIdAsync(contactId).ConfigureAwait(false);

            return _mapper.Map<ViewContactDTO>(record);
        }

        public async Task<bool> UpdateAsync(int contactId, UpdateContactDTO contact)
        {
            try
            {
                var record = await _unitofwork.Contacts.GetByIdAsync(contactId).ConfigureAwait(false);
                if (record == null)
                    return false;

                _mapper.Map(contact, record);

                _unitofwork.CreateTransaction();
                await _unitofwork.Contacts.UpdateAsync(record).ConfigureAwait(false);
                _unitofwork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _unitofwork.Rollback();
                _logger.LogError($@"{ex.Message}");
                return false;
            }
        }
    }
}
