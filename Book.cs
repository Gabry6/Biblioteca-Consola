using System;

namespace BibliotecaConsola
{
    /// <summary>
    /// Representa un libro dentro del catálogo de la biblioteca.
    /// </summary>
    class Book
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnioPublicacion { get; set; }
        public string Genero { get; set; }

        public Book(int id, string titulo, string autor, int anioPublicacion, string genero)
        {
            Id = id;
            Titulo = titulo;
            Autor = autor;
            AnioPublicacion = anioPublicacion;
            Genero = genero;
        }

        public override string ToString()
        {
            return string.Format(
                "ID: {0,-3} | {1,-28} | {2,-20} | {3,4} | {4}",
                Id, Titulo, Autor, AnioPublicacion, Genero
            );
        }
    }
}
