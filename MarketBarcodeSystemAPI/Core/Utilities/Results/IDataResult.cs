﻿namespace MarketBarcodeSystemAPI.Core.Utilities.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; }
    }
}
