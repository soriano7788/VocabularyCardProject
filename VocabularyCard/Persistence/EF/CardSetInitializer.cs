using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Domain;

namespace VocabularyCard.Persistence.EF
{
    public class CardSetInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EFCardSetDao>
    {
        protected override void Seed(EFCardSetDao context)
        {
            var cards = new List<CardSet>
            {
                new CardSet
                {
                    Flag=1,
                    DisplayName="美劇",
                    CreatedDateTime=DateTime.UtcNow,
                    ModifiedDateTime=DateTime.UtcNow,
                    Creator="admin",
                    Modifier="admin",
                    State=CardSetState.Active
                },
                new CardSet
                {
                    Flag=1,
                    DisplayName="動畫",
                    CreatedDateTime=DateTime.UtcNow,
                    ModifiedDateTime=DateTime.UtcNow,
                    Creator="admin",
                    Modifier="admin",
                    State=CardSetState.Active
                },
            };

            cards.ForEach(s => context.CardSets.Add(s));
            // It isn't necessary to call the SaveChanges method after each group of entities, 
            // as is done here, but doing that helps you locate the source of a problem 
            // if an exception occurs while the code is writing to the database.
            context.SaveChanges();
        }
    }
}
