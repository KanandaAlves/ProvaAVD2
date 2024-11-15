using System.ComponentModel;

namespace Venda.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }
        public DateTime DataPedido { get; set; }
        public decimal Total { get; set; }

        [DisplayName("Nome do Cliente")]
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public ICollection<ItemPedido>? ItensPedidos { get; set; }
    }
}
