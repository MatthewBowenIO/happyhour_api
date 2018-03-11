using System;
namespace happyhour_api.Models
{
    public class GiphyImages
    {
        public GiphyImage fixed_height { get; set; }
        public GiphyImage fixed_height_still { get; set; }
        public GiphyImage fixed_height_downsampled { get; set; }
        public GiphyImage fixed_width { get; set; }
        public GiphyImage fixed_width_still { get; set; }
        public GiphyImage fixed_width_downsampled { get; set; }
        public GiphyImage fixed_height_small { get; set; }
        public GiphyImage fixed_width_small { get; set; }
        public GiphyImage original { get; set; }
    }
}
