using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Entidades
{
    public enum Descricao
    {
        [Description("Pintura")]
        Pintura = 'P',
        [Description("Limpeza")]
        Limpeza = 'L',
        [Description("Troca de lâmpadas")]
        TrocaDeLampada = 'T'

    }
}