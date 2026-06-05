using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication1.Repositories
{
    public abstract class BaseRepository
    {
    protected readonly string _StrConexao = ConfigurationManager.ConnectionStrings["LabraConnection"].ConnectionString;
                
    }
}