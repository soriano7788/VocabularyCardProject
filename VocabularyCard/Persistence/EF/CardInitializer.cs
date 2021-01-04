using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Persistence.EF
{
    public class CardInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EFCardDao>
    {
        protected override void Seed(EFCardDao context)
        {
            var cards = new List<Card>
            {
                new Card
                {
                    Flag=1,
                    Vocabulary="apple",
                    CreatedDateTime=DateTime.UtcNow,
                    ModifiedDateTime=DateTime.UtcNow,
                    Creator="admin",
                    Modifier="admin",
                    State=CardState.Active
                },
                new Card
                {
                    Flag=1,
                    Vocabulary="bird",
                    CreatedDateTime=DateTime.UtcNow,
                    ModifiedDateTime=DateTime.UtcNow,
                    Creator="admin",
                    Modifier="admin",
                    State=CardState.Active
                },
            };

            cards.ForEach(s => context.Cards.Add(s));
            // It isn't necessary to call the SaveChanges method after each group of entities, 
            // as is done here, but doing that helps you locate the source of a problem 
            // if an exception occurs while the code is writing to the database.
            context.SaveChanges();
        }
    }
}
