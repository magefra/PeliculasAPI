namespace PeliculasAPI.DTOs
{
    public class PaginacionDTO
    {

        /// <summary>
        /// 
        /// </summary>
        public int Pagina { get; set; } = 1;


        /// <summary>
        /// 
        /// </summary>
        private int cantidadRegistroPagina = 10;

        /// <summary>
        /// 
        /// </summary>
        private readonly int cantidadMaximaregistrosPagina = 50;


        /// <summary>
        /// 
        /// </summary>
        public int CantidadRegistroPagina
        {
            get => cantidadRegistroPagina;
            set
            {
                cantidadRegistroPagina = (value > cantidadMaximaregistrosPagina) ?
                                          cantidadMaximaregistrosPagina :
                                          value;
            }
        }

    }
}
