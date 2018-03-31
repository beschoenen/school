<div style="height:200px;" class="image-carousel">
    <div class="image-carousel">
        @forelse($images as $image)
            <div>
                <div class="image-center">
                    <img src="{{ route('misc.thumb', $image->id) }}" alt="{{ $image->name }}"/>
                </div>
            </div>
        @empty
            <div class="no-img">
                <p>Geen foto's</p>
            </div>
        @endforelse
    </div>
</div>

@push('css')
    <link rel="stylesheet" type="text/css" href="{{ asset('/assets/packages/slick/slick.css') }}"/>
    <link rel="stylesheet" type="text/css" href="{{ asset('/assets/packages/slick/slick-theme.css') }}"/>
@endpush

@push('js')
    <script src="{{ asset('/assets/packages/slick/slick.min.js') }}"></script>
    <script>
        $(".image-carousel").slick({
            dots: true,
            infinite: true,
            fade: true,
            cssEase: 'linear',
            centerMode: true,
            swipe: false,
            autoplay: true,
            autoplaySpeed: 3000,
            arrows: false
        });
    </script>

    @if($editMode)
        <script>
            new ImageModal({
                apiUrl: "{{ route('back.api.listImages') }}",
                imgUrl: "{{ route('misc.thumb', 'img-id') }}",
                multiMode: true,
                button: ".image-carousel img, .image-carousel .no-img",
                selected: "{{ implode(',', $images->lists('id')->toArray()) }}",
                done: function(ids) {
                    $.ajax({
                        url: "{{ route('back.api.images') }}",
                        type: "POST",
                        dataType: "json",
                        data: {
                            page: "{{ $page }}",
                            images: ids
                        },
                        headers: { 'X-CSRF-TOKEN': $('meta[name="csrf-token"]').attr('content') }
                    }).complete(function(object) {
                        if(object.status !== 200) {
                            alert("Er ging iets fout tijdens het opslaan!");
                        } else {
                            // Remove all slides
                            $(".image-carousel").slick('removeSlide', null, null, true);

                            if(ids.length > 0) {
                                ids.forEach(function(item) {
                                    $(".image-carousel").slick('addSlide', (function() {
                                        var container = document.createElement('div');

                                        var center = document.createElement('div');
                                        center.classList.add('image-center');

                                        var image = document.createElement('img');
                                        image.src = "{{ route('misc.thumb', 'img-id') }}".replace(/img-id/, item);

                                        center.appendChild(image);
                                        container.appendChild(center);

                                        return container;
                                    })());
                                });
                            } else {
                                $(".image-carousel").slick('addSlide', (function() {
                                    var container = document.createElement('div');
                                    container.classList.add('no-img');

                                    var text = document.createElement('p');
                                    text.innerText = "Geen foto's";

                                    container.appendChild(text);

                                    return container;
                                })());
                            }
                            $(".image-carousel").slick('slickGoTo', 0);
                        }
                    });
                }
            });
        </script>
    @endif
@endpush