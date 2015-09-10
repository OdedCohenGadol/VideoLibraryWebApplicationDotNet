using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Generic.Interfaces;

namespace Generic.Model
{
    /// <summary>
    /// Generic model type
    /// </summary>
    /// <typeparam name="E">E - entity</typeparam>
    /// <typeparam name="M">M - Model type of GenericModel</typeparam>
    public class GenericModel<E, M> : IGenericModel<E, M> where M : IGenericModel<E, M>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public GenericModel()
        {

        }

        /// <summary>
        /// Constructor that build the model from the entity
        /// </summary>
        /// <param name="entity"></param>
        public GenericModel(E entity)
        {
            if (entity != null)
            {
                /// get E and M types
                Type entityType = typeof(E);
                Type modelType = typeof(M);

                /// create instance of E
                // M model = (M)Activator.CreateInstance(typeof(M));

                IList<PropertyInfo> entityProps = new List<PropertyInfo>(entityType.GetProperties());

                foreach (PropertyInfo entityProp in entityProps)
                {
                    PropertyInfo modelProp = this.GetType().GetProperty(entityProp.Name, BindingFlags.Public | BindingFlags.Instance);
                  
                    if (null != modelProp && modelProp.CanWrite)
                    {
                        var entityValue = entityProp.GetValue(entity);
                        if (modelProp.PropertyType.BaseType != null && modelProp.PropertyType.BaseType.Name.Contains("GenericModel") && entityValue != null)
                        {
                            //var method = modelProp.PropertyType.GetMethod("GetModel");
                            entityValue = Activator.CreateInstance(modelProp.PropertyType, new object[] { entityValue });
                        }
                        else if (entityProp.PropertyType.Name.Contains("Collection") && entityValue != null)
                        {
                            var listType = modelProp.PropertyType.GetGenericArguments()[0];
                            var method = listType.BaseType.GetMethod("GetListOfModels", BindingFlags.Public | BindingFlags.Static);
                            entityValue = method.Invoke(null, new object[] { entityValue });
                        }

                        modelProp.SetValue(this, entityValue, null);
                    }
                }
            }
        }

        /// <summary>
        /// Map the model from the entity
        /// </summary>
        /// <param name="entity"></param>
        public virtual M GetModel(E entity)
        {
            /// get E and M types
            Type entityType = typeof(E);
            Type modelType = typeof(M);

            /// create instance of M with E
            M model = (M)Activator.CreateInstance(typeof(M), entity);
            return model;
        }

        /// <summary>
        /// Map the model from the entity, diffrent from the E entity 
        /// </summary>
        /// <param name="entity">type of the entity</param>
        public static M GetModel<Entity>(Entity entity)
        {
            /// get E and M types
            Type entityType = typeof(Entity);
            Type modelType = typeof(M);

            M model = (M)Activator.CreateInstance(typeof(M));

            if (entity != null)
            {
                /// create instance of E
                // M model = (M)Activator.CreateInstance(typeof(M));

                IList<PropertyInfo> entityProps = new List<PropertyInfo>(entityType.GetProperties());

                foreach (PropertyInfo entityProp in entityProps)
                {
                    PropertyInfo modelProp = model.GetType().GetProperty(entityProp.Name, BindingFlags.Public | BindingFlags.Instance);

                    if (null != modelProp && modelProp.CanWrite)
                    {
                        var entityValue = entityProp.GetValue(entity);
                        if (modelProp.PropertyType.BaseType != null && modelProp.PropertyType.BaseType.Name.Contains("GenericModel"))
                        {
                            //var method = modelProp.PropertyType.GetMethod("GetModel");

                            entityValue = Activator.CreateInstance(modelProp.PropertyType, new object[] { entityValue });
                        }
                        else if (entityProp.PropertyType.Name.Contains("Collection") && entityValue != null)
                        {
                            var listType = modelProp.PropertyType.GetGenericArguments()[0];
                            var method = listType.BaseType.GetMethod("GetListOfModels", BindingFlags.Public | BindingFlags.Static);
                            entityValue = method.Invoke(null, new object[] { entityValue });
                        }

                        modelProp.SetValue(model, entityValue, null);
                    }
                }
            }

            return model;
        }

        /// <summary>
        /// Get the entity from the model
        /// </summary>
        /// <returns></returns>
        public virtual E GetEntity()
        {
            /// get E and M types
            Type entityType = typeof(E);
            Type modelType = typeof(M);

            /// create instance of E
            E entity = (E)Activator.CreateInstance(typeof(E));

            IList<PropertyInfo> modelProps = new List<PropertyInfo>(modelType.GetProperties());

            foreach (PropertyInfo modelProp in modelProps)
            {
                PropertyInfo entityProp = entity.GetType().GetProperty(modelProp.Name, BindingFlags.Public | BindingFlags.Instance);
                if (null != entityProp && entityProp.CanWrite)
                {
                    var modelValue = modelProp.GetValue(this);
                    if (modelProp.PropertyType.BaseType.Name.Contains("GenericModel") && modelValue != null)
                    {
                        var method = modelProp.PropertyType.GetMethod("GetEntity");
                        modelValue = method.Invoke(modelValue, null);
                    }
                    else if (modelProp.PropertyType.Name.Contains("List") && modelValue != null)
                    {
                        var listType = modelProp.PropertyType.GetGenericArguments()[0];
                        var method = listType.BaseType.GetMethod("GetListOfEntities", BindingFlags.Public | BindingFlags.Static);
                        modelValue = method.Invoke(null, new object[] { modelValue });
                    }

                    entityProp.SetValue(entity, modelValue, null);
                }
            }

            return entity;
        }


        /// <summary>
        /// Get a list of entities from the model
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<E> GetListOfEntities(List<M> models)
        {
            var result = new List<E>();

            result = models.Select(m => m.GetEntity()).ToList();

            return result;
        }

        /// <summary>
        /// Get a list of models from the entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static List<M> GetListOfModels(ICollection<E> entities)
        {
            var result = new List<M>();
            //M model = (M)Activator.CreateInstance(typeof(M));

            //modelType
            Type modelType = typeof(M);
            result = entities.Select(e => (M)Activator.CreateInstance(modelType, new object[] { e })).ToList();

            return result;
        }
    }
           

    //public static class GenericModelExtensions
    //{
    //    /// <summary>
    //    /// Get a list of entities from the model
    //    /// </summary>
    //    /// <typeparam name="TSource"></typeparam>
    //    /// <typeparam name="TResult"></typeparam>
    //    /// <param name="models"></param>
    //    /// <returns></returns>
    //    public static List<TResult> ToListOfEntities<TSource, TResult>(this IEnumerable<TSource> models) where TSource : IGenericModel<TResult, TSource>
    //    {
    //        var result = new List<TResult>();

    //        result = models.Select(m => m.GetEntity()).ToList();

    //        return result;
    //    }

    //    /// <summary>
    //    /// Get a list of models from the entities
    //    /// </summary>
    //    /// <typeparam name="TSource"></typeparam>
    //    /// <typeparam name="TResult"></typeparam>
    //    /// <param name="entities"></param>
    //    /// <returns></returns>
    //    public static List<TResult> ToListOfModels<TSource, TResult>(this ICollection<TSource> entities) where TResult : IGenericModel<TSource, TResult>
    //    {
    //        var result = new List<TResult>();
    //        TResult model = (TResult)Activator.CreateInstance(typeof(TResult));

    //        result = entities.Select(e => model.GetModel(e)).ToList();

    //        return result;
    //    }

    //}

}
