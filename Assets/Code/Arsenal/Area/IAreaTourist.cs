using System;
using UnityEngine;

namespace Code.Arsenal.Area
{
    /// <summary>
    /// The interface of Tourist that can enter an area or a shape
    /// </summary>
    public interface IAreaTourist
    {
        Vector3 Position { get; }
        Quaternion Rotation { get; }
    }

    /// <summary>
    /// The interface of Tourist Sensor, which can sense the tourist enter or exist the area or shape
    /// </summary>
    public interface IAreaTouristSensor
    {
        /// <summary>
        /// Callback when a tourist enter the area or shape
        /// </summary>
        Action<IAreaTourist> OnTouristEnter { get; set; }

        /// <summary>
        /// Callback when a tourist exist the area or shape
        /// </summary>
        Action<IAreaTourist> OnTouristExit { get; set; }

        /// <summary>
        /// Check whether the given tourist is in this area or shape
        /// </summary>
        /// <param name="area_tourist">the given tourist</param>
        /// <returns>true when the given tourist is in the area or shape</returns>
        bool IsTouristInArea(IAreaTourist area_tourist);

        /// <summary>
        /// Check whether the given tourist can enter the area or shape
        /// </summary>
        /// <param name="area_tourist">the given tourist</param>
        /// <returns>true if the given tourist can enter the area or shape</returns>
        bool IsTouristCanEnter(IAreaTourist area_tourist);

        /// <summary>
        /// Check whether the given tourist can exist the area or shape
        /// </summary>
        /// <param name="area_tourist">the given tourist</param>
        /// <returns>true if the given tourist can exist the area or shape</returns>
        bool IsTouristCanExit(IAreaTourist area_tourist);
    }
}