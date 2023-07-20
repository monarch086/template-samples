﻿using System.Data;
using static Dapper.SqlMapper;

namespace UID.Persistance;

public class GuidUlidHandler : TypeHandler<Ulid>
{
    public override Ulid Parse(object value)
    {
        return new Ulid((byte[])value);
    }

    public override void SetValue(IDbDataParameter parameter, Ulid value)
    {
        parameter.DbType = DbType.Guid;
        parameter.Size = 16;
        parameter.Value = value.ToGuid();
    }
}
