using System;

namespace BibliotecaConsola
{
    class Program
    {
        // Arreglo de tamaño fijo: estructura principal de almacenamiento (capacidad máxima = 20)
        static Book[] catalogo = new Book[20];
        static int cantidad = 0;      // Cantidad de libros actualmente registrados
        static int siguienteId = 1;   // Contador para generar IDs autoincrementales

        static void Main(string[] args)
        {
            Console.OutputEncoding = new System.Text.UTF8Encoding(false);
            CargarLibrosIniciales();
            Menu();
        }

        // ------------------------------------------------------------------
        // Carga inicial: registra 10 libros de ejemplo para cumplir con el
        // requisito de un catálogo mínimo de 10 libros al iniciar el sistema.
        // ------------------------------------------------------------------
        static void CargarLibrosIniciales()
        {
            AgregarLibroInterno("Cien Años de Soledad", "Gabriel García Márquez", 1967, "Realismo mágico");
            AgregarLibroInterno("Don Quijote de la Mancha", "Miguel de Cervantes", 1605, "Novela clásica");
            AgregarLibroInterno("1984", "George Orwell", 1949, "Distopía");
            AgregarLibroInterno("El Principito", "Antoine de Saint-Exupéry", 1943, "Fábula");
            AgregarLibroInterno("Rayuela", "Julio Cortázar", 1963, "Novela experimental");
            AgregarLibroInterno("Crónica de una Muerte Anunciada", "Gabriel García Márquez", 1981, "Novela corta");
            AgregarLibroInterno("Fahrenheit 451", "Ray Bradbury", 1953, "Ciencia ficción");
            AgregarLibroInterno("La Casa de los Espíritus", "Isabel Allende", 1982, "Realismo mágico");
            AgregarLibroInterno("El Túnel", "Ernesto Sábato", 1948, "Novela psicológica");
            AgregarLibroInterno("Pedro Páramo", "Juan Rulfo", 1955, "Realismo mágico");
        }

        // Variante interna usada solo para precargar datos sin pasar por el menú.
        static void AgregarLibroInterno(string titulo, string autor, int anio, string genero)
        {
            catalogo[cantidad] = new Book(siguienteId, titulo, autor, anio, genero);
            cantidad++;
            siguienteId++;
        }

        // ------------------------------------------------------------------
        // Menú principal
        // ------------------------------------------------------------------
        static void Menu()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine();
                Console.WriteLine("====== SISTEMA DE BIBLIOTECA ======");
                Console.WriteLine();
                Console.WriteLine("1. Agregar libro");
                Console.WriteLine("2. Listar libros");
                Console.WriteLine("3. Buscar libro por ID");
                Console.WriteLine("4. Actualizar libro");
                Console.WriteLine("5. Eliminar libro");
                Console.WriteLine("0. Salir");
                Console.WriteLine();
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1": AgregarLibro(); break;
                    case "2": ListarLibros(); break;
                    case "3": BuscarLibroPorId(); break;
                    case "4": ActualizarLibro(); break;
                    case "5": EliminarLibro(); break;
                    case "0": salir = true; Console.WriteLine("Saliendo del sistema..."); break;
                    default: Console.WriteLine("Opción inválida. Intente de nuevo."); break;
                }
            }
        }

        // ------------------------------------------------------------------
        // 1) AGREGAR
        // Big O: O(1) — el libro se inserta directamente en la posición
        // "cantidad" (siguiente espacio disponible). No requiere recorrer
        // el arreglo, solo verificar si hay espacio (comparación constante).
        // ------------------------------------------------------------------
        static void AgregarLibro()
        {
            if (cantidad >= catalogo.Length)
            {
                Console.WriteLine("No se puede agregar el libro: el catálogo alcanzó su capacidad máxima de " + catalogo.Length + " libros.");
                return;
            }

            Console.Write("Título: ");
            string titulo = Console.ReadLine();

            Console.Write("Autor: ");
            string autor = Console.ReadLine();

            Console.Write("Año de publicación: ");
            int anio = int.Parse(Console.ReadLine());

            Console.Write("Género: ");
            string genero = Console.ReadLine();

            catalogo[cantidad] = new Book(siguienteId, titulo, autor, anio, genero); // O(1)
            cantidad++;
            siguienteId++;

            Console.WriteLine("Libro agregado correctamente con ID " + (siguienteId - 1) + ".");
        }

        // ------------------------------------------------------------------
        // 2) LISTAR
        // Big O: O(n) — se debe recorrer todo el arreglo (n = cantidad de
        // libros registrados) para mostrar cada elemento una vez.
        // ------------------------------------------------------------------
        static void ListarLibros()
        {
            if (cantidad == 0)
            {
                Console.WriteLine("El catálogo no tiene libros registrados.");
                return;
            }

            Console.WriteLine("Catálogo de libros (" + cantidad + "/" + catalogo.Length + "):");
            Console.WriteLine(new string('-', 90));
            for (int i = 0; i < cantidad; i++) // recorrido O(n)
            {
                Console.WriteLine(catalogo[i]);
            }
            Console.WriteLine(new string('-', 90));
        }

        // ------------------------------------------------------------------
        // 3) BUSCAR POR ID
        // Big O: O(n) — búsqueda lineal; en el peor caso (libro al final del
        // arreglo o inexistente) se deben revisar los n elementos.
        // ------------------------------------------------------------------
        static int BuscarIndicePorId(int id)
        {
            for (int i = 0; i < cantidad; i++) // O(n) en el peor caso
            {
                if (catalogo[i].Id == id)
                {
                    return i;
                }
            }
            return -1; // No encontrado
        }

        static void BuscarLibroPorId()
        {
            Console.Write("Ingrese el ID del libro a buscar: ");
            int id = int.Parse(Console.ReadLine());

            int indice = BuscarIndicePorId(id); // O(n)

            if (indice == -1)
            {
                Console.WriteLine("No existe ningún libro registrado con el ID " + id + ".");
            }
            else
            {
                Console.WriteLine("Libro encontrado:");
                Console.WriteLine(catalogo[indice]);
            }
        }

        // ------------------------------------------------------------------
        // 4) ACTUALIZAR
        // Big O: O(n) — requiere primero localizar el libro mediante
        // búsqueda lineal O(n); una vez encontrado, la modificación de sus
        // campos es O(1). El costo total de la operación es O(n).
        // ------------------------------------------------------------------
        static void ActualizarLibro()
        {
            Console.Write("Ingrese el ID del libro a actualizar: ");
            int id = int.Parse(Console.ReadLine());

            int indice = BuscarIndicePorId(id); // O(n)

            if (indice == -1)
            {
                Console.WriteLine("No existe ningún libro registrado con el ID " + id + ".");
                return;
            }

            Book libro = catalogo[indice];
            Console.WriteLine("Libro actual:");
            Console.WriteLine(libro);
            Console.WriteLine("Ingrese los nuevos datos (deje vacío para conservar el valor actual):");

            Console.Write("Título [" + libro.Titulo + "]: ");
            string titulo = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(titulo)) libro.Titulo = titulo; // O(1)

            Console.Write("Autor [" + libro.Autor + "]: ");
            string autor = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(autor)) libro.Autor = autor; // O(1)

            Console.Write("Año de publicación [" + libro.AnioPublicacion + "]: ");
            string anioStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(anioStr)) libro.AnioPublicacion = int.Parse(anioStr); // O(1)

            Console.Write("Género [" + libro.Genero + "]: ");
            string genero = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(genero)) libro.Genero = genero; // O(1)

            Console.WriteLine("Libro actualizado correctamente.");
        }

        // ------------------------------------------------------------------
        // 5) ELIMINAR
        // Big O: O(n) — localizar el libro es O(n) en el peor caso, y el
        // desplazamiento de los elementos posteriores para cerrar el hueco
        // dejado también es O(n) en el peor caso (eliminar el primero
        // desplaza n-1 elementos). El costo total de la operación es O(n).
        // ------------------------------------------------------------------
        static void EliminarLibro()
        {
            Console.Write("Ingrese el ID del libro a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            int indice = BuscarIndicePorId(id); // O(n)

            if (indice == -1)
            {
                Console.WriteLine("No existe ningún libro registrado con el ID " + id + ".");
                return;
            }

            string tituloEliminado = catalogo[indice].Titulo;

            // Desplazamiento hacia la izquierda: O(n) en el peor caso
            for (int i = indice; i < cantidad - 1; i++)
            {
                catalogo[i] = catalogo[i + 1];
            }

            catalogo[cantidad - 1] = null; // Libera la última posición duplicada
            cantidad--;

            Console.WriteLine("Libro \"" + tituloEliminado + "\" eliminado correctamente.");
        }
    }
}
