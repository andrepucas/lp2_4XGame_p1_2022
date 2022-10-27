using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>Water Tile</c> Class.
/// Contains all info about each individual water tile.
/// </summary>
public class WaterTile : GameTile
{
    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// monetary value of each water tile.
    /// </summary>
    /// <value>Monetary value of the game tile.</value>
    public override int Coin {get;}

    /// <summary>
    /// Read only self implemented property that sets and stores the base
    /// food production value of each water tile.
    /// </summary>
    /// <value>Food Production of the game tile.</value>
    public override int Food {get;}

     /// <summary>
    /// Overrides IEnumerable<Resource> and stores it in resourceList.
    /// </summary>
    public override IEnumerable<Resource> Resources => resourceList;

    /// <summary>
    /// Creates a list of the Resource type.
    /// </summary>
    /// <typeparam name="Resource"></typeparam>
    /// <returns>Stores resources in current tile.</returns>
    private List<Resource> resourceList = new List<Resource>();

    /// <summary>
    /// Adds resource to resourceList.
    /// </summary>
    /// <param name="resource"></param>
    public override void AddResource(Resource resource)
    {
        resourceList.Add(resource);
    }

    /// <summary>
    /// Creates the tile's resources and adds them to the respective list.
    /// </summary>
    public override void CreateResources()
    {
        // Stores number or resources to be added.
        int numberOfResources = Random.Range(0, 6);

        // Debug message that shows the number of resources to be added.
        Debug.Log($"Number of Resources: {numberOfResources}");

        // Variables that check if resource has already been added.
        bool animals = false;
        bool fossil = false;
        bool luxury = false;
        bool metals = false;
        bool plants = false;
        bool pollution = false;

        // Runs until determined number of resources are added.
        while (resourceList.Count != numberOfResources)
        {
            // Saves variable that determines the resource to be added.
            int rndResource = Random.Range(0, 6);

            // Saves generic resource to be used later.
            Resource currentResource;

            // Checks if it's the previously generated number (rndResource) and
            // if the resource has been already added.
            if (rndResource == 0 && animals == false)
            {
                // Updates variable stating that a resource of this type
                // has been created.
                animals = true;

                // Creates and saves appropriate resource in variable.
                currentResource = new AnimalsResource();

                // Adds appropriate resource to the resourceList.
                AddResource(currentResource);
            }

            // Checks if it's the previously generated number (rndResource) and
            // if the resource has been already added.
            if (rndResource == 1 && fossil == false)
            {
                // Updates variable stating that a resource of this type
                // has been created.
                fossil = true;

                // Creates and saves appropriate resource in variable.
                currentResource = new FossilFuelResource();

                // Adds appropriate resource to the resourceList.
                AddResource(currentResource);
            }

            // Checks if it's the previously generated number (rndResource) and
            // if the resource has been already added.
            if (rndResource == 2 && luxury == false)
            {
                // Updates variable stating that a resource of this type
                // has been created.
                luxury = true;

                // Creates and saves appropriate resource in variable.
                currentResource = new LuxuryResource();

                // Adds appropriate resource to the resourceList.
                AddResource(currentResource);
            }

            // Checks if it's the previously generated number (rndResource) and
            // if the resource has been already added.
            if (rndResource == 3 && metals == false)
            {
                // Updates variable stating that a resource of this type
                // has been created.
                metals = true;

                // Creates and saves appropriate resource in variable.
                currentResource = new MetalsResource();

                // Adds appropriate resource to the resourceList.
                AddResource(currentResource);
            }

            // Checks if it's the previously generated number (rndResource) and
            // if the resource has been already added.
            if (rndResource == 4 && plants == false)
            {
                // Updates variable stating that a resource of this type
                // has been created.
                plants = true;

                // Creates and saves appropriate resource in variable.
                currentResource = new PlantsResource();

                // Adds appropriate resource to the resourceList.
                AddResource(currentResource);
            }

            // Checks if it's the previously generated number (rndResource) and
            // if the resource has been already added.
            if (rndResource == 5 && pollution == false)
            {
                // Updates variable stating that a resource of this type
                // has been created.
                pollution = true;

                // Creates and saves appropriate resource in variable.
                currentResource = new PollutionResource();

                // Adds appropriate resource to the resourceList.
                AddResource(currentResource);
            }
        }
    }

    /// <summary>
    /// Constructor method, instantiates a new Desert Tile.
    /// </summary>
    /// <param name="Coin">Desert Tile's monetary value.</param>
    /// <param name="Food">Desert Tile's food production value.</param>
    /// <param name="totalResourceCoin">
    /// Total monetary value of the tile's resources.</param>
    /// <param name="grid">
    /// Total food production value of the tile's resources.</param>
    public WaterTile()
    {
        // Integers that stores total monetary and food production values of
        // resources.
        int totalResourceCoin = 0;
        int totalResourceFood = 0;

        // Creates tile's resources.
        CreateResources();

        // Goes through each of the tile's resources.
        foreach (Resource resource in resourceList)
        {
            // Sums resources total monetary and food production values.
            totalResourceCoin += resource.Coin;
            totalResourceFood += resource.Food;
        }

        // Saves tile's total monetary and food production values.
        this.Coin = 0 + totalResourceCoin;
        this.Food = 1 + totalResourceFood;
    }

    /// <summary>
    /// Shows all of the tile's important information.
    /// </summary>
    /// <remarks>Specially useful for debugging.</remarks>
    /// <returns>A string with all of the tile's info</returns>
    public override string ToString()
    {
        // Shows resource introduction sentence.
        Debug.Log("---RESOURCES---");

        // Goes through each resource.
        foreach (Resource resource in resourceList)
        {
            // Shows relevant resource information.
            Debug.Log($"Type: {resource.GetType()} / Coin: {resource.Coin} / Food: {resource.Food}");
        }

        // Shows game tile introduction sentence.
        Debug.Log("---GAME TILE---");

        // Shows general tile information.
        return $"Type: {this.GetType()} / Coin: {this.Coin} / Food: {this.Food}";
    }
}
