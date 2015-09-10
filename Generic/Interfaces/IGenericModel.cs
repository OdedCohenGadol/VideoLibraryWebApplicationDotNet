using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generic.Interfaces
{
    /// <summary>
    /// Generic model type
    /// </summary>
    /// <typeparam name="E">E - entity</typeparam>
    /// <typeparam name="M">M - Model type of GenericModel</typeparam>
    public interface IGenericModel<E,M>
    {
        /// <summary>
        /// Map the model from the entity
        /// </summary>
        /// <param name="entity"></param>
        M GetModel(E entity);


        /// <summary>
        /// Get the entity from the model
        /// </summary>
        /// <returns></returns>
        E GetEntity();
    }
}
