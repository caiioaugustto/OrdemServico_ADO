using System.ComponentModel;

namespace Entidades
{
    public enum Prazo
    {
        [Description("5 Dias")]
        CincoDias,
        [Description("7 Dias")]
        SeteDias,
        [Description("Uma Semana")]
        UmaSemana,
    }
}