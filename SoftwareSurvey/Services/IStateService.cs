﻿using SoftwareSurvey.Models;
using System.Threading.Tasks;

namespace SoftwareSurvey.Services
{
    public interface IStateService
    {
        T GetOrNew<T>() where T : IStateObject, new();
        void Save<T>(T state) where T : IStateObject;

        Task<bool> Persist();
    }
}