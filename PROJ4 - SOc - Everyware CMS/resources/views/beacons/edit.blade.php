@extends('layouts.app')

@section('content')
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                @if($errors->any())
                    <div class="alert alert-danger">
                        <ul>
                            @foreach ($errors->all() as $error)
                                <li>{{ $error }}</li>
                            @endforeach
                        </ul>
                    </div>
                @endif
                <div class="panel panel-default">
                    <div class="panel-heading">Edit Beacon</div>
                    <div class="panel-body">
                        <form method="post" action="{{ route('beacons.update', $beacon->beacon->_id) }}">
                            {{ method_field('PUT') }}
                            {{ csrf_field() }}

                            <div class="row">
                                <div class="col-md-12">
                                    <label for="name" class="control-label">Beacon naam</label>
                                    <input type="text" id="name" name="name" class="form-control"
                                           value="{{ $beacon->data->name ?? '' }}"/>
                                </div>
                            </div>

                            <h3>Points of Interest</h3>

                            <div id="pois">

                                @foreach($beacon->data->pointsOfInterest ?? [] as $pointOfInterest)

                                    <div class="poi">
                                        <h4 class="poi-header" id="poi-{{ $loop->iteration }}">
                                            <a href="#poi-{{ $loop->iteration }}">#{{ $loop->iteration }}</a>
                                            <span onclick="this.closest('.poi').remove()" class="clickable pull-right">
                                                <i class="fa fa-close"></i>
                                            </span>
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="pointsOfInterest[{{ $loop->index }}][name]"
                                                       class="control-label">Naam</label>
                                                <input type="text" class="form-control"
                                                       id="pointsOfInterest[{{ $loop->index }}][name]"
                                                       name="pointsOfInterest[{{ $loop->index }}][name]"
                                                       value="{{ $pointOfInterest->name ?? '' }}"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="pointsOfInterest[{{ $loop->index }}][direction]"
                                                       class="control-label">Richting</label>
                                                <input type="number" class="form-control" min="1" max="36"
                                                       id="pointsOfInterest[{{ $loop->index }}][direction]"
                                                       name="pointsOfInterest[{{ $loop->index }}][direction]"
                                                       value="{{ $pointOfInterest->direction ?? '' }}"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="pointsOfInterest[{{ $loop->index }}][description]"
                                                       class="control-label">Beschrijving</label>
                                                <input type="text" class="form-control"
                                                       id="pointsOfInterest[{{ $loop->index }}][description]"
                                                       name="pointsOfInterest[{{ $loop->index }}][description]"
                                                       value="{{ $pointOfInterest->description ?? '' }}"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="pointsOfInterest[{{ $loop->index }}][spokenDescription]"
                                                       class="control-label">Gesproken beschrijving</label>
                                                <input type="text" class="form-control"
                                                       id="pointsOfInterest[{{ $loop->index }}][spokenDescription]"
                                                       name="pointsOfInterest[{{ $loop->index }}][spokenDescription]"
                                                       value="{{ $pointOfInterest->spokenDescription ?? '' }}"/>
                                            </div>
                                        </div>
                                    </div>

                                @endforeach
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <br>
                                    <button type="button" class="btn btn-default" onclick="newPoi()">
                                        POI <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>

                            <h3>Gelinkte Beacons</h3>

                            <div id="lbs">
                                @foreach($beacon->data->linkedBeacon ?? [] as $linkedBeacon)

                                    <div class="lb">
                                        <h4 class="lb-header" id="lb-{{ $loop->iteration }}">
                                            <a href="#lb-{{ $loop->iteration }}">#{{ $loop->iteration }}</a>
                                            <span onclick="this.closest('.lb').remove()" class="clickable pull-right">
                                                            <i class="fa fa-close"></i>
                                                        </span>
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="linkedBeacon[{{ $loop->index }}][beacon]"
                                                       class="control-label">Naar beacon</label>
                                                <select class="form-control" name="linkedBeacon[{{ $loop->index }}][beacon]">
                                                    @foreach($beacons as $b)
                                                        @isset($b->data->name)
                                                            @if($b->beacon->_id == $linkedBeacon->beacon)
                                                                <option value="{{ $b->beacon->_id}}" selected>{{ $b->data->name }}</option>
                                                            @else
                                                                <option value="{{ $b->beacon->_id }}">{{ $b->data->name }}</option>
                                                            @endif
                                                        @endisset
                                                    @endforeach
                                                </select>
                                            </div>
                                        </div>

                                        <button type="button" class="btn btn-default" onclick="newLbInstruction({{ $loop->index }})">
                                            Instructie <i class="fa fa-plus"></i>
                                        </button>

                                        @foreach($linkedBeacon->instructions ?? [] as $instruction)
                                            <div class="sublb">
                                                <h6 id="lb-{{ $loop->parent->iteration }}-sublb-{{ $loop->iteration }}">
                                                    <a href="#lb-{{ $loop->parent->iteration }}-sublb-{{ $loop->iteration }}">Instructie #{{ $loop->iteration }}</a>
                                                    <span onclick="this.closest('.sublb').remove()" class="clickable pull-right">
                                                        <i class="fa fa-close"></i>
                                                    </span>
                                                </h6>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <label for="linkedBeacon[{{ $loop->parent->index }}][instructions][{{ $loop->index }}][from]"
                                                               class="control-label">Vanaf beacon</label>
                                                        <select class="form-control" name="linkedBeacon[{{ $loop->parent->index }}][instructions][{{ $loop->index }}][from]">
                                                            @foreach($beacons as $b)
                                                                @isset($b->data->name)
                                                                    @if($b->beacon->_id == $instruction->from)
                                                                        <option value="{{ $b->beacon->_id}}" selected>{{ $b->data->name }}</option>
                                                                    @else
                                                                        <option value="{{ $b->beacon->_id }}">{{ $b->data->name }}</option>
                                                                    @endif
                                                                @endisset
                                                            @endforeach
                                                        </select>
                                                    </div>
                                                </div>
    
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <label for="linkedBeacon[{{ $loop->parent->index }}][instructions][{{ $loop->index }}][instruction]"
                                                               class="control-label">Instructie</label>
                                                        <input type="text" class="form-control"
                                                               id="linkedBeacon[{{ $loop->parent->index }}][instructions][{{ $loop->index }}][instruction]"
                                                               name="linkedBeacon[{{ $loop->parent->index }}][instructions][{{ $loop->index }}][instruction]"
                                                               value="{{ $instruction->instruction ?? '' }}"/>
                                                    </div>
                                                </div>
        
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <label for="linkedBeacon[{{ $loop->parent->index }}][instructions][{{ $loop->index }}][direction]"
                                                               class="control-label">Richting</label>
                                                        <select class="form-control" id="linkedBeacon[{{ $loop->parent->index }}][instructions][{{ $loop->index }}][direction]" name="linkedBeacon[{{ $loop->parent->index }}][instructions][{{ $loop->index }}][direction]" >
                                                            <option value="0" {{ ($instruction->direction == 0) ? 'selected':'' }} >↑</option>
                                                            <option value="1" {{ ($instruction->direction == 1) ? 'selected':'' }} >↗</option>
                                                            <option value="2" {{ ($instruction->direction == 2) ? 'selected':'' }} >↱</option>
                                                            <option value="3" {{ ($instruction->direction == 3) ? 'selected':'' }} >↘</option>
                                                            <option value="4" {{ ($instruction->direction == 4) ? 'selected':'' }} >↓</option>
                                                            <option value="5" {{ ($instruction->direction == 5) ? 'selected':'' }} >↙</option>
                                                            <option value="6" {{ ($instruction->direction == 6) ? 'selected':'' }} >↰</option>
                                                            <option value="7" {{ ($instruction->direction == 7) ? 'selected':'' }} >↖</option>
                                                            <option value="8" {{ ($instruction->direction == 8) ? 'selected':'' }} >○</option>
                                                        </select>
                                                    </div>
                                                </div>
                                            </div>
                                        @endforeach
                                    </div>
                                @endforeach
                            </div>

                            <br/>
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="btn btn-default" onclick="newLb()">
                                        Linked Beacon <i class="fa fa-plus"></i>
                                    </button>
                                    <div class="pull-right">
                                        <a href="{{ route('beacons.show', $beacon->beacon->_id) }}"
                                           class="btn btn-default">Annuleren</a>
                                        <input type="submit" value="Opslaan" class="btn btn-primary"/>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
@endsection

@section('js')
    <script>
        function newPoi () {
            var POIs = $('#pois');
            var noPOIs = POIs.find('.poi').length;

            var poi = $('<div class="poi"></div>')[0];

            poi.append($(
                '<h4 id="poi-' + (noPOIs + 1) + '">' +
                    '<a href="#poi-' + (noPOIs + 1) + '">#' + (noPOIs + 1) + '</a>' +
                    '<span onclick="this.closest(\'.poi\').remove()" class="clickable pull-right">' +
                        '<i class="fa fa-close"></i>' +
                    '</span>' +
                '</h4>'
            )[0]);

            poi.append(makeInput('Naam', 'pointsOfInterest', 'name', noPOIs, 'text'));
            poi.append(makeInput('Richting', 'pointsOfInterest', 'direction', noPOIs, 'number'));
            poi.append(makeInput('Beschrijving', 'pointsOfInterest', 'description', noPOIs, 'text'));
            poi.append(makeInput('Gesproken beschrijving', 'pointsOfInterest', 'spokenDescription', noPOIs, 'text'));

            POIs.append(poi);
        }

        function newLb () {
            var LBs = $('#lbs');
            var noLBs = LBs.find('.lb').length;
            console.log(noLBs);

            var lb = $('<div class="lb"></div>')[0];

            lb.append($(
                '<h4 id="poi-' + (noLBs + 1) + '">' +
                '<a href="#poi-' + (noLBs + 1) + '">#' + (noLBs + 1) + '</a>' +
                '<span onclick="this.closest(\'.lb\').remove()" class="clickable pull-right">' +
                '<i class="fa fa-close"></i>' +
                '</span>' +
                '</h4>'
            )[0]);

            lb.append(makeSelect('Naar beacon', 'linkedBeacon', 'beacon', noLBs, 'text'));

            lb.append($(
                '<button type="button" class="btn btn-default" onclick="newLbInstruction(' + noLBs + ')">' +
                    'Instructie <i class="fa fa-plus"></i>' +
                '</button>'
            )[0]);

            LBs.append(lb);
        }

        function newLbInstruction(noLBs) {
            var LBs = $('#lbs');
            var lb = LBs.find('.lb')
            var noSubLBs = lb.eq(noLBs).find('.sublb').length;
            console.log(noSubLBs);

            var sublb = $('<div class="sublb"></div>')[0];

            sublb.append($(
                '<h6 id="lb-' + (noLBs + 1) + '-sublb-' + (noSubLBs + 1) + '">' +
                '<a href="#lb-' + (noLBs + 1) + '-sublb-' + (noSubLBs + 1) + '">Instructie #' + (noSubLBs + 1) + '</a>' +
                '<span onclick="this.closest(\'.sublb\').remove()" class="clickable pull-right">' +
                '<i class="fa fa-close"></i>' +
                '</span>' +
                '</h6>'
            )[0]);

            sublb.append(makeSubSelect('Vanaf beacon', 'linkedBeacon', 'from', noLBs, noSubLBs, 'text'));
            sublb.append(makeSubInput('Instructie', 'linkedBeacon', 'instruction', noLBs, noSubLBs, 'text'));
            sublb.append(makeSelectDirection(noLBs, noSubLBs));

            lb.eq(noLBs).append(sublb);
        }

        function makeInput(title, lbfor, name, index, type) {
            return $(
                '<div class="row">' +
                    '<div class="col-md-12">' +
                        '<label for="' + lbfor + '[' + index + '][' + name + ']" class="control-label">' +
                            title +
                        '</label>' +
                        '<input type="' + type + '" class="form-control" ' +
                            ((type === 'number') ? 'min="1" max="36" ' : '') +
                            'id="' + lbfor + '[' + index + '][' + name + ']" ' +
                            'name="' + lbfor + '[' + index + '][' + name + ']"/>' +
                    '</div>' +
                '</div>'
            )[0];
        }

        function makeSelect(title, lbfor, name, index) {
            return $(
                '<div class="row">' +
                '<div class="col-md-12">' +
                '<label for="' + lbfor + '[' + index + '][' + name + ']" class="control-label">' +
                title +
                '</label>' +
                '<select class="form-control" ' +
                'name="' + lbfor + '[' + index + '][' + name + ']">' +
                @foreach($beacons as $b)
                        @isset($b->data->name)
                            '<option value="{{ $b->beacon->_id }}">{{ $b->data->name }}</option>'+
                        @endisset
                    @endforeach
                '</select>' +
                '</div>' +
                '</div>'
            )[0];
        }

        function makeSubInput(title, lbfor, name, index, subindex, type) {
            return $(
                '<div class="row">' +
                    '<div class="col-md-12">' +
                        '<label for="' + lbfor + '[' + index + '][instructions][' + subindex + '][' + name + ']" class="control-label">' +
                            title +
                        '</label>' +
                        '<input type="' + type + '" class="form-control" ' +
                            ((type === 'number') ? 'min="1" max="36" ' : '') +
                            'id="' + lbfor + '[' + index + '][instructions][' + subindex + '][' + name + ']" ' +
                            'name="' + lbfor + '[' + index + '][instructions][' + subindex + '][' + name + ']"/>' +
                    '</div>' +
                '</div>'
            )[0];
        }

        function makeSubSelect(title, lbfor, name, index, subindex) {
            return $(
                '<div class="row">' +
                '<div class="col-md-12">' +
                '<label for="' + lbfor + '[' + index + '][instructions][' + subindex + '][' + name + ']" class="control-label">' +
                title +
                '</label>' +
                '<select class="form-control" ' +
                'name="' + lbfor + '[' + index + '][instructions][' + subindex + '][' + name + ']">' +
                @foreach($beacons as $b)
                        @isset($b->data->name)
                            '<option value="{{ $b->beacon->_id }}">{{ $b->data->name }}</option>'+
                        @endisset
                    @endforeach
                '</select>' +
                '</div>' +
                '</div>'
            )[0];
        }


        function makeSelectDirection(index, subindex) {
            return $(
                '<div class="row">' +
                '<div class="col-md-12">' +
                '<label for="linkedBeacon[' + index + '][instructions][' + subindex + '][direction]" class="control-label">Richting</label>' +
                '<select class="form-control" name="linkedBeacon[' + index + '][instructions][' + subindex + '][direction]" >' +
                '<option value="0">↑</option>'+
                '<option value="1">↗</option>'+
                '<option value="2">↱</option>'+
                '<option value="3">↘</option>'+
                '<option value="4">↓</option>'+
                '<option value="5">↙</option>'+
                '<option value="6">↰</option>'+
                '<option value="7">↖</option>'+
                '<option value="8">○</option>'+
                '</select>' +
                '</div>' +
                '</div>'
            )[0];
        }
    </script>
@endsection
