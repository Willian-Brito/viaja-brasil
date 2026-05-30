using ViajaBrasil.Domain.Entities;

namespace ViajaBrasil.Infrastructure.Seeds;

public static class TouristSpotsSeed
{
    public static TouristSpot[] Data =>
    [
        new TouristSpot(
            "Cristo Redentor",
            "Estátua localizada no topo do Morro do Corcovado.",
            "Parque Nacional da Tijuca",
            "3304557" // Rio de Janeiro/RJ
        ),

        new TouristSpot(
            "Pão de Açúcar",
            "Complexo de morros com vista panorâmica da cidade.",
            "Urca",
            "3304557" // Rio de Janeiro/RJ
        ),

        new TouristSpot(
            "Cataratas do Iguaçu",
            "Conjunto de quedas d'água na fronteira com a Argentina.",
            "Parque Nacional do Iguaçu",
            "4108304" // Foz do Iguaçu/PR
        ),

        new TouristSpot(
            "Pelourinho",
            "Centro histórico com casarões coloniais coloridos.",
            "Centro Histórico",
            "2927408" // Salvador/BA
        ),

        new TouristSpot(
            "Elevador Lacerda",
            "Cartão postal que liga a Cidade Alta à Cidade Baixa.",
            "Praça Tomé de Souza",
            "2927408" // Salvador/BA
        ),

        new TouristSpot(
            "Teatro Amazonas",
            "Importante patrimônio histórico da região amazônica.",
            "Centro de Manaus",
            "1302603" // Manaus/AM
        ),

        new TouristSpot(
            "Avenida Paulista",
            "Principal centro financeiro e cultural da cidade.",
            "Bela Vista",
            "3550308" // São Paulo/SP
        )
    ];
}