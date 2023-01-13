using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.PruebaTecnicaContext context = new DL.PruebaTecnicaContext())
                {
                    var getAllQuery = context.Departamentos.FromSqlRaw($"DepartamentoGetAll").ToList();
                    if (getAllQuery != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in getAllQuery)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.NombreDepartamento = obj.Nombre;
                            result.Objects.Add(departamento);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
