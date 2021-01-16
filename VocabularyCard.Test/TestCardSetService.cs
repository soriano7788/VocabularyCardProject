using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VocabularyCard.Core;
using VocabularyCard.Core.EF;
using VocabularyCard.Dtos;
using VocabularyCard.Repositories;
using VocabularyCard.Repositories.EF;
using VocabularyCard.Services;
using VocabularyCard.Services.Impl;
using VocabularyCard.AccountManager.DTO;

namespace VocabularyCard.Test
{
    [TestFixture]
    public class TestCardSetService
    {
        private ICardSetService _cardSetService;
        private ICardSetRepository _cardSetRepository;
        private ICardRepository _cardRepository;
        IUnitOfWork _unitOfWork;

        // 2.x 版時是 TestFixtureSetUp
        [OneTimeSetUp]
        public void TestCaseInit()
        {
            //DbContext context = new BaseDbContext();
            //_unitOfWork = new EFUnitOfWork(context);
            //_cardSetRepository = new EFCardSetRepository(context);
            //_cardSetService = new CardSetService(_unitOfWork, _cardSetRepository, _cardRepository);
            //Console.WriteLine("*************TestCaseInit************");
        }

        [Test]
        public void Test_000_GetById(UserInfo user, int id)
        {
            //Arrange
            UserInfo testUser = new UserInfo { UserId = "admin" };
            int inputId = 1;

            //Act
            CardSetDto dto =  _cardSetService.GetById(testUser, inputId);
            int actual = dto.Id;

            //Assert
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }
    }
}
