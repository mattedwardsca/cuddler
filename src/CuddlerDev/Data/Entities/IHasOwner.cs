﻿namespace CuddlerDev.Data.Entities;

public interface IHasOwner
{
    string Id { get; set; }

    string OwnerId { get; set; }
}