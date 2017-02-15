using System;
using System.Collections.Generic;
using EventCaptureApp.Models;
using Newtonsoft.Json;

namespace EventCaptureApp
{
	public class TestData
	{
		public TestData()
		{
		}


		public static string GetCampaignList()
		{
			List<CampaignOverview> campaignList = new List<CampaignOverview>()
			{
				new CampaignOverview() {
					Id = 1,
					Name = "Campaign 1",
					DateModified = DateTime.Now,
					ConfigFileName = "campaign1.json"
				},
				new CampaignOverview() {
					Id = 2,
					Name = "Campaign 2",
					DateModified = DateTime.Now,
					ConfigFileName = "campaign2.json"
				},
				new CampaignOverview() {
					Id = 3,
					Name = "Campaign 3",
					DateModified = DateTime.Now,
					ConfigFileName = "campaign3.json"
				},
				new CampaignOverview() {
					Id = 4,
					Name = "Campaign 4",
					DateModified = DateTime.Now,
					ConfigFileName = "campaign4.json"
				}
			};
			return JsonConvert.SerializeObject(campaignList);
		}


		public static string GetCampaign()
		{
			Campaign campaign = new Campaign()
			{
				Id = 1,
				Name = "Campaign 1",
				DateModified = DateTime.Now,
				ConfigFileName = "campaign1.json",
				Categories = new List<CampaignCategory>() {
					new CampaignCategory() {
						Id = 1,
						Title = "Campaign Category 1",
						ThumbImageName = "",
						Documents = new List<CampaignDocument>() {
							new CampaignDocument() { Id = 1, Title = "Category 1: Document 1", FileName = "doc11.pdf"},
							new CampaignDocument() { Id = 2, Title = "Category 1: Document 2", FileName = "doc12.pdf"},
							new CampaignDocument() { Id = 3, Title = "Category 1: Document 3", FileName = "doc13.pdf"}
						}
					},
					new CampaignCategory() {
						Id = 2,
						Title = "Campaign Category 2",
						ThumbImageName = "",
						Documents = new List<CampaignDocument>() {
							new CampaignDocument() { Id = 11, Title = "Category 2: Document 1", FileName = "doc21.pdf"},
							new CampaignDocument() { Id = 12, Title = "Category 2: Document 2", FileName = "doc22.pdf"},
							new CampaignDocument() { Id = 13, Title = "Category 2: Document 3", FileName = "doc23.pdf"}
						}
					},
					new CampaignCategory() {
						Id = 3,
						Title = "Campaign Category 3",
						ThumbImageName = "",
						Documents = new List<CampaignDocument>() {
							new CampaignDocument() { Id = 21, Title = "Category 3: Document 1", FileName = "doc31.pdf"},
							new CampaignDocument() { Id = 22, Title = "Category 3: Document 2", FileName = "doc32.pdf"},
							new CampaignDocument() { Id = 23, Title = "Category 3: Document 3", FileName = "doc33.pdf"}
						}
					},
					new CampaignCategory() {
						Id = 4,
						Title = "Campaign Category 4",
						ThumbImageName = "",
						Documents = new List<CampaignDocument>() {
							new CampaignDocument() { Id = 31, Title = "Category 4: Document 1", FileName = "doc41.pdf"},
							new CampaignDocument() { Id = 32, Title = "Category 4: Document 2", FileName = "doc42.pdf"},
							new CampaignDocument() { Id = 33, Title = "Category 4: Document 3", FileName = "doc43.pdf"}
						}
					}
				}
			};
			return JsonConvert.SerializeObject(campaign);
		}
	}
}
