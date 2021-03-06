﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoVoyages.Model
{
    /**
     * Datastructure class.
     * Provides data attributes for this classs and via a facade pattern calls the appropirate database methods.
     */

    public class Destination : Table
    {
        private Destination_db destination_db = new Destination_db();
        private int destinationId;
        private string continent;
        private string pays;
        private string region;
        private string description;

        public Destination() { }

        public Destination(int destinationId, string continent, string pays, string region, string description)
        {
            this.destinationId = destinationId;
            this.continent = continent;
            this.pays = pays;
            this.region = region;
            this.description = description;
        }

        public int DestinationId { get => destinationId; set => destinationId = value; }
        public string Continent { get => continent; set => continent = value; }
        public string Pays { get => pays; set => pays = value; }
        public string Region { get => region; set => region = value; }
        public string Description { get => description; set => description = value; }

        public override void startTransaction()
        {
            destination_db.startTransaction();
        }

        public override int endTransaction(bool commit)
        {
            return destination_db.endTransaction(commit);
        }

        public Destination getDestination(int destinationId)
        {
            return destination_db.getDestination(destinationId);
        }

        public List<Destination> getDestinations(string key, string value)
        {
            return destination_db.getDestinations(key, value);
        }

        public List<Destination> getDestinations()
        {
            return destination_db.getDestinations();
        }

        public int updateDestination(string change, string condition)
        {
            return destination_db.updateDestination(change, condition);
        }

        public int deleteDestination(int destinationId)
        {
            return destination_db.deleteDestination(destinationId);
        }

        public int insertDestination(Destination destination)
        {
            return destination_db.insertDestination(destination);
        }

    }
}
