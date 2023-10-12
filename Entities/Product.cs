﻿namespace LR3.Entities;

public class Product : BaseEntity
{
    public string Title { get; set; }

    public string Description { get; set; }

    public ProductType ProductType { get; set; }

    public double Price { get; set; }
}

public enum ProductType
{
    Dress = 0,
    Jeans = 1,
    Tshirt = 2,
    Coat = 3,
    Jacket = 4,
    DownJacket = 5
}