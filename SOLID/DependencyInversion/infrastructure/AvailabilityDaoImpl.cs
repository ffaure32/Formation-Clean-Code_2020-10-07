namespace SOLID.DependencyInversion.infrastructure
{
    public class AvailabilityDaoImpl : IAvailabilityDao {
    
        public bool IsAvailable() {
            //En realite il y aurait une dependance vers une base de donn�esS...
            return true; 
        }
    
    }
}
