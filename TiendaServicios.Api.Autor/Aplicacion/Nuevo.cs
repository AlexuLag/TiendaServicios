﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;
using TiendaServicios.Api.Autor.Persistencia;

namespace TiendaServicios.Api.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }

        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _contexto;

            public Manejador(ContextoAutor contexto)
            {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro
                {
                     Apellido=request.Apellido,
                     Nombre = request.Nombre,
                     FechaNacimiento=request.FechaNacimiento,
                     AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };

                _contexto.Autorlibro.Add(autorLibro);

                var valor = await _contexto.SaveChangesAsync();
                if (valor > 0)
                    return Unit.Value;
                throw new Exception("No se pudo insertar el autor del libro");




            }
        }
    }
}