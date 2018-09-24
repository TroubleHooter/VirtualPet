using System;

namespace VirtualPet.Repository.XMLModels
{
    public abstract class XmlModel
    {
        protected XmlModel()
        {
            Id = new Guid();
        }

        protected XmlModel(Guid id)
        {
            Id = id;
        }

        public Guid Id { get;}
    }
}
