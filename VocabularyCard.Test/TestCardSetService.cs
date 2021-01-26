using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VocabularyCard.CacheProvider;
using VocabularyCard.Core;
using VocabularyCard.Core.EF;
using VocabularyCard.Dtos;
using VocabularyCard.Repositories;
using VocabularyCard.Repositories.EF;
using VocabularyCard.Services;
using VocabularyCard.Services.Impl;
using VocabularyCard.AccountManager.DTO;
using NSubstitute;
using VocabularyCard.Entities;

namespace VocabularyCard.Test
{
    [TestFixture]
    public class TestCardSetService
    {
        private ICacheProvider _cacheProvider;
        private ICardSetService _cardSetService;
        private ICardSetRepository _cardSetRepository;
        private ICardRepository _cardRepository;
        IUnitOfWork _unitOfWork;

        // 2.x 版時是 TestFixtureSetUp
        [OneTimeSetUp]
        public void TestCaseInit()
        {
            // 這邊應該要忽略 DbContext，因為 DbContext 是 Repository、UnitOfWork 依賴的 instance
            // 這邊目標是測試 CardSetService，只關注 CardSetService 和其 methods，
            // 連 Repository、UnitOfWork 用 mock 了
            _cacheProvider = Substitute.For<ICacheProvider>();
            _cardSetRepository = Substitute.For<ICardSetRepository>();
            _cardRepository = Substitute.For<ICardRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            _cardSetService = new CardSetService(_unitOfWork, _cardSetRepository, _cardRepository, _cacheProvider);
            //DbContext context = new BaseDbContext();
            //_unitOfWork = new EFUnitOfWork(context);
            //_cardSetRepository = new EFCardSetRepository(context);
            //_cardSetService = new CardSetService(_unitOfWork, _cardSetRepository, _cardRepository);
            //Console.WriteLine("*************TestCaseInit************");
        }

        [Test]
        public void Test_000_Get_CardSet_By_Id_1_return_1()
        {
            //Arrange
            UserInfo testUser = new UserInfo { UserId = "admin" };
            int inputId = 1;
            _cardSetRepository.GetByCardSetId(inputId).Returns(new CardSet { CardSetId = inputId, Owner = "admin" });

            //Act
            CardSetDto dto = _cardSetService.GetById(testUser, inputId);
            int actual = dto.Id;

            //Assert
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }
    }
}
