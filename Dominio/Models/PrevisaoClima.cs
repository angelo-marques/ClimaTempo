﻿using System;

namespace Dominio.Models
{
    public class PrevisaoClima
    {
        public int Id { get; set; }
        public int CidadeId { get; set; }
        public DateTime DataPrevisao { get; set; }
        public string Clima { get; set; }
        public decimal TemperaturaMinima { get; set; }
        public decimal TemperaturaMaxima { get; set; }
        public Cidade Cidade { get; set; }
    }
}
