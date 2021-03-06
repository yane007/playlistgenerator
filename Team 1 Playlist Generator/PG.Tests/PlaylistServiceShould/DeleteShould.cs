﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PG.Data.Context;
using PG.Services;
using PG.Services.DTOs;
using PG.Services.Exceptions;
using PG.Services.Helpers;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PG.Tests.PlaylistServiceShould
{
    [TestClass]
    public class DeleteShould
    {
        [TestMethod]
        public async Task DeleteCorrectly()
        {
            var options = Utils.GetOptions(nameof(DeleteCorrectly));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var playlist = new PlaylistDTO
            {
                Title = "In Utero",
                Duration = 1600,
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(playlist);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);
                await sut.Delete(1);

                var playlists = await sut.GetAllPlaylists();
                int playlistsCount = playlists.Count();

                Assert.AreEqual(0, playlistsCount);
            }
        }

        [TestMethod]
        public async Task DeleteThrowsWhenInvalidId()
        {
            var options = Utils.GetOptions(nameof(DeleteThrowsWhenInvalidId));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var assertContext = new PGDbContext(options);

            var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

            await Assert.ThrowsExceptionAsync<OutOfRangeException>(() => sut.Delete(-1));
            await Assert.ThrowsExceptionAsync<OutOfRangeException>(() => sut.Delete(0));
        }

        [TestMethod]
        public async Task DeleteThrowsWhenIdDoesNotExist()
        {
            var options = Utils.GetOptions(nameof(DeleteThrowsWhenIdDoesNotExist));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var playlist = new PlaylistDTO
            {
                Title = "In Utero",
                Duration = 1600,
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);

                await sut.Create(playlist);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                await Assert.ThrowsExceptionAsync<NotFoundException>(() => sut.Delete(2));
            }
        }

        [TestMethod]
        public async Task DeleteThrowsWhenIdAlreadyDeleted()
        {
            var options = Utils.GetOptions(nameof(DeleteThrowsWhenIdAlreadyDeleted));
            var pixabayServiceMock = new Mock<IPixabayService>();

            var playlist = new PlaylistDTO
            {
                Title = "In Utero",
                Duration = 1600, 
            };

            using (var arrangeContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(arrangeContext, pixabayServiceMock.Object);
                await sut.Create(playlist);
                await arrangeContext.SaveChangesAsync();
            }

            using (var assertContext = new PGDbContext(options))
            {
                var sut = new PlaylistService(assertContext, pixabayServiceMock.Object);

                await sut.Delete(1);

                await Assert.ThrowsExceptionAsync<ArgumentException>(() => sut.Delete(1));
            }
        }
    }
}
