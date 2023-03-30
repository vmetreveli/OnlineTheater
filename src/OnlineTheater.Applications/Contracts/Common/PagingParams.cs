﻿namespace OnlineTheater.Applications.Contracts.Common;

public sealed class PagingParams
{
    private const int MAX_PAGE_SIZE = 50;
    private int _pageSize = 10;
    public int PageNumber { get; set; } = 1;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
    }
}