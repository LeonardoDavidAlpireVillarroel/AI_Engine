using System;

namespace AIEngine.Decision.Blackboard
{
    [Serializable]
    public class BlackboardData
    {
        public string key;         // clave identificadora
        public Type type;            // tipo real de dato
        private object valor;        // valor genérico (puede ser cualquier cosa)

        public BlackboardData(string key, object valor)
        {
            this.key = key;
            this.valor = valor;
            this.type = valor?.GetType();
        }

        public T GetValue<T>()
        {
            if (valor is T typedValue)
            {
                return typedValue;
            }
            throw new InvalidCastException($"No se puede convertir el valor de la clave '{key}' al tipo {typeof(T).Name}");
        }

        public void SetValue<T>(T newValue)
        {
            valor = newValue;
            type = typeof(T);
        }

        public object GetRawValue()
        {
            return valor;
        }
    }
}
