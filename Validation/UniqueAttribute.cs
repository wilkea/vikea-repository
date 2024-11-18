using System.ComponentModel.DataAnnotations;
using dpcleague_2.Data;
using Microsoft.EntityFrameworkCore;

namespace dpcleague_2.Validation
{
    public class UniqueAttribute : ValidationAttribute
    {
        private readonly string _propertyName;
        private readonly Type _entityType;

        public UniqueAttribute(Type entityType, string propertyName)
        {
            _entityType = entityType;
            _propertyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = (dpcContext)validationContext.GetService(typeof(dpcContext));
            var property = value?.ToString();

            if (string.IsNullOrEmpty(property))
                return ValidationResult.Success;

            var entityType = dbContext.Model.FindEntityType(_entityType);
            var dbSet = dbContext.GetType().GetProperty(entityType.GetTableName()).GetValue(dbContext);

            var existingEntity = ((IQueryable<object>)dbSet)
                .Where(e => EF.Property<string>(e, _propertyName) == property)
                .FirstOrDefault();

            if (existingEntity != null)
            {
                // Check if we're updating an existing entity
                var currentEntity = validationContext.ObjectInstance;
                var idProperty = _entityType.GetProperty($"{_entityType.Name}Id");
                
                if (idProperty != null)
                {
                    var currentId = (int)idProperty.GetValue(currentEntity);
                    var existingId = (int)idProperty.GetValue(existingEntity);

                    if (currentId == existingId)
                        return ValidationResult.Success;
                }

                return new ValidationResult($"Acest {_propertyName.ToLower()} există deja în baza de date.");
            }

            return ValidationResult.Success;
        }
    }
} 