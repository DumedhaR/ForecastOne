import useEmblaCarousel from "embla-carousel-react";
import Autoplay from "embla-carousel-autoplay";
import Fade from "embla-carousel-fade";
import "../styles/slider.css";

interface HeroFadeSliderProps {
  images: string[];
}

export default function FadeSlider({ images }: HeroFadeSliderProps) {
  const [emblaRef] = useEmblaCarousel({ loop: true }, [
    Fade(),
    Autoplay({ delay: 5000, stopOnInteraction: false }),
  ]);

  return (
    <div className="relative w-full h-full overflow-hidden">
      {/* Background image fade slider */}
      <div className="absolute inset-0 z-0" ref={emblaRef}>
        <div className="embla__container h-full">
          {images.map((src, i) => (
            <div key={i} className="embla__slide">
              <img
                src={src}
                alt={`slide${i}`}
                className="embla__slide__img w-full h-full object-cover"
              />
            </div>
          ))}
        </div>
      </div>

      {/* Overlay */}
      <div className="absolute inset-0 bg-black/20 z-10" />
    </div>
  );
}
