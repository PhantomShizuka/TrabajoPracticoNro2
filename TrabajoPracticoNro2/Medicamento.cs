using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoPracticoNro2
{
    public class Medicamento
    {
        private string _base;

        public Medicamento(string Base)
        {
            _base = Base;
        }

        public DataTable MostrarDatos()
        {
            return ConexionSQL.FillData("select * from Medicamento", _base);
        }

        public void GenerarNuevoId(Control control)
        {
            var controls = new List<Control>
            {
                control
            };

            ExecuteProcedure("usp_generaCod_Medicamento", controls);
        }

        public void AgregarMedicamento(List<Control> controls)
        {
            ExecuteProcedure("usp_guardar_Medicamento", controls);
        }

        private void ExecuteProcedure(string usp, List<Control> controls)
        {
            var values_input = controls.Select(n => ObjectUtil.GetDefaulValue(n)).ToList();

            var values_output = ConexionSQL.ExecuteProcedure(usp, _base, values_input);

            for (int i = 0; i < values_output.Count; i++)
            {
                if (values_output[i] != null)
                {
                    ObjectUtil.SetDefaulValue(controls[i], values_output[i]);
                }
            }
        }
    }
}