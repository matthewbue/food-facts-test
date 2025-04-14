using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Text.Json.Serialization;

namespace FitnessFoods.Domain.Entities
{
    public enum ProductStatus { Draft, Trash, Published }

    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonIgnore] // This won't be set from JSON
        public ProductStatus Status { get; set; } = ProductStatus.Published;

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [JsonIgnore] // This will be set on import, not from JSON
        public DateTime ImportedT { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("creator")]
        public string? Creator { get; set; }

        [JsonPropertyName("created_t")]
        public long CreatedT { get; set; }

        [JsonPropertyName("last_modified_t")]
        public long LastModifiedT { get; set; }

        [JsonPropertyName("product_name")]
        public string? ProductName { get; set; }

        [JsonPropertyName("quantity")]
        public string? Quantity { get; set; }

        [JsonPropertyName("brands")]
        public string? Brands { get; set; }

        [JsonPropertyName("categories")]
        public string? Categories { get; set; }

        [JsonPropertyName("labels")]
        public string? Labels { get; set; }

        [JsonPropertyName("cities")]
        public string? Cities { get; set; }

        [JsonPropertyName("purchase_places")]
        public string? PurchasePlaces { get; set; }

        [JsonPropertyName("stores")]
        public string? Stores { get; set; }

        [JsonPropertyName("ingredients_text")]
        public string? IngredientsText { get; set; }

        [JsonPropertyName("traces")]
        public string? Traces { get; set; }

        [JsonPropertyName("serving_size")]
        public string? ServingSize { get; set; }

        [JsonPropertyName("serving_quantity")]
        public string? ServingQuantity { get; set; }

        [JsonPropertyName("nutriscore_score")]
        public string? NutriscoreScore { get; set; }

        [JsonPropertyName("nutriscore_grade")]
        public string? NutriscoreGrade { get; set; }

        [JsonPropertyName("main_category")]
        public string? MainCategory { get; set; }

        [JsonPropertyName("image_url")]
        public string? ImageUrl { get; set; }

        // Método para tratar e corrigir valores de nutriscore_score
        public int? GetNutriscoreScore()
        {
            if (int.TryParse(NutriscoreScore, out int result))
            {
                return result;
            }
            return null; // Se não for válido, retorna null
        }

        // Método para tratar e corrigir valores de serving_quantity
        public double? GetServingQuantity()
        {
            if (double.TryParse(ServingQuantity, out double result))
            {
                return result;
            }
            return null; // Se não for válido, retorna null
        }
    }
}
