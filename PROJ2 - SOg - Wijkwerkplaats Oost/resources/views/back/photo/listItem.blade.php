<li class="list-group-item photo-list-item">
    <div class="row">
        <div class="col-md-1">
            <input title="delete image" type="checkbox" name="photo[]" value="{{ $photo->id }}"/>
        </div>
        <div class="col-md-4">
            <img class="photo-list-image" alt="{{ $photo->name }}" src="{{ route('misc.thumb', $photo->id) }}"/>
        </div>
        <div class="col-md-5">
            <span>{{ $photo->name }}</span>
        </div>
        <div class="col-md-2">
            <div class="row">
                <div class="col-md-12 col-sm-5 col-xs-5">
                    <a class="pull-right" href="{{ route('back.photo.edit', $photo->id) }}">Bewerk</a>
                </div>
                <div class="hidden-md hidden-lg col-sm-1 col-xs-1">
                    <span> | </span>
                </div>
                <div class="col-md-12 col-sm-5 col-xs-5">
                    <a class="pull-xs-left pull-sm-left pull-md-right pull-lg-right" href="{{ route('back.photo.delete', $photo->id) }}">Verwijder</a>
                </div>
            </div>
        </div>
    </div>
</li>