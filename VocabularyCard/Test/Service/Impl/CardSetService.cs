using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyCard.Test.Repository;
using VocabularyCard.Domain;

namespace VocabularyCard.Test.Service.Impl
{
    public class CardSetService : BaseService, ICardSetService
    {
        private ICardSetRepository _cardSetRepository;
        private ICardRepository _cardRepository;

        public CardSetService(IUnitOfWork unitOfWork, ICardSetRepository cardSetRepository, ICardRepository cardRepository) : base(unitOfWork)
        {
            _cardSetRepository = cardSetRepository;
            _cardRepository = cardRepository;
        }

        public void TestUnitOfWork()
        {
            CardSet cardSet = new CardSet
            {
                Flag = 1,
                DisplayName = "Test Unit Of Work",
                Description = string.Empty,
                CreatedDateTime = DateTime.UtcNow,
                ModifiedDateTime = DateTime.UtcNow,
                Creator = "test",
                Modifier = "test",
                Owner = "test",
                State = CardSetState.Active
            };
            //IRepository<CardSet> _repository = UnitOfWork.Repository<CardSet>();
            //_repository.Create(cardSet);
            _cardSetRepository.Create(cardSet);


            // 再來測試同時加 cardset 和 card，然後在中間丟 exception
            //throw new Exception("zzz");

            Card card = new Card
            {
                Flag = 1,
                Vocabulary = "Test Unit Of Work",
                CreatedDateTime = DateTime.UtcNow,
                ModifiedDateTime = DateTime.UtcNow,
                Creator = "test",
                Modifier = "test",
                State = CardState.Active
            };
            _cardRepository.Create(card);

            UnitOfWork.Save();
        }

    }
}
