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
                    <div class="panel-heading">Edit Tour</div>
                    <div class="panel-body">
                        <form method="post" action="{{ route('tours.update', $tour->id) }}">
                            {{ method_field('PUT') }}
                            {{ csrf_field() }}

                            <div class="row">
                                <div class="col-md-12">
                                    <label for="name" class="control-label">Tour naam</label>
                                    <input type="text" id="name" name="name" class="form-control" value="{{ $tour->name }}" required />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <label for="spoken_description" class="control-label">Uitgesproken beschrijving</label>
                                    <input type="text" id="spoken_description" name="spoken_description" class="form-control" value="{{ $tour->spoken_description }}" required />
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <label for="written_description" class="control-label">Op scherm geschreven beschrijving</label>
                                    <input type="text" id="written_description" name="written_description" class="form-control" value="{{ $tour->written_description }}" />
                                </div>
                            </div>

                            <h3>Tour punten</h3>

                            <div id="nodes">
                                @foreach($tour->nodes as $node)
                                    <div class="node">
                                        <h4 class="node-header" id="node-{{ $loop->iteration }}">
                                            <a href="#node-{{ $loop->iteration }}">#{{ $loop->iteration }}</a>
                                            <span onclick="this.closest('.node').remove()" class="clickable pull-right">
                                                <i class="fa fa-close"></i>
                                            </span>
                                        </h4>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="nodes[{{ $loop->index }}][node]"
                                                       class="control-label">Node</label>
                                                <select class="form-control" name="nodes[{{ $loop->index }}][node]" required>
                                                    @foreach($beacons as $b)
                                                        @isset($b->data->name)
                                                            <option value="{{ $b->beacon->_id}}"
                                                                    @if($b->beacon->_id == $node->node)
                                                                        selected
                                                                    @endif
                                                            >{{ $b->data->name }}</option>
                                                        @endisset
                                                    @endforeach
                                                </select>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="nodes[{{ $loop->index }}][spoken_description]"
                                                       class="control-label">Uitgesproken beschrijving</label>
                                                <input type="text" class="form-control" required
                                                       id="nodes[{{ $loop->index }}][spoken_description]"
                                                       name="nodes[{{ $loop->index }}][spoken_description]"
                                                       value="{{ $node->spoken_description }}"/>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="nodes[{{ $loop->index }}][written_description]"
                                                       class="control-label">Op scherm geschreven beschrijving</label>
                                                <input type="text" class="form-control"
                                                       id="nodes[{{ $loop->index }}][written_description]"
                                                       name="nodes[{{ $loop->index }}][written_description]"
                                                       value="{{ $node->written_description ?? '' }}"/>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-12">
                                                <label for="nodes[{{ $loop->index }}][direction]"
                                                       class="control-label">Richting</label>
                                                <select class="form-control" id="nodes[{{ $loop->index }}][direction]" name="nodes[{{ $loop->index }}][direction]" required>
                                                    <option value="0" {{ ($node->direction == 0) ? 'selected':'' }} >↑</option>
                                                    <option value="1" {{ ($node->direction == 1) ? 'selected':'' }} >↗</option>
                                                    <option value="2" {{ ($node->direction == 2) ? 'selected':'' }} >↱</option>
                                                    <option value="3" {{ ($node->direction == 3) ? 'selected':'' }} >↘</option>
                                                    <option value="4" {{ ($node->direction == 4) ? 'selected':'' }} >↓</option>
                                                    <option value="5" {{ ($node->direction == 5) ? 'selected':'' }} >↙</option>
                                                    <option value="6" {{ ($node->direction == 6) ? 'selected':'' }} >↰</option>
                                                    <option value="7" {{ ($node->direction == 7) ? 'selected':'' }} >↖</option>
                                                    <option value="8" {{ ($node->direction == 8) ? 'selected':'' }} >○</option>
                                                </select>
                                            </div>
                                        </div>
                                    </div>
                                @endforeach
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <br>
                                    <button type="button" class="btn btn-default" onclick="newnode()">
                                        node <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                            </div>

                            <br/>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="pull-right">
                                        <a href="{{ route('tours.show', $tour->id) }}"
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
        function newnode () {
            var nodes = $('#nodes');
            var nonodes = nodes.find('.node').length;

            var node = $('<div class="node"></div>')[0];

            node.append($(
                '<h4 id="node-' + (nonodes + 1) + '">' +
                '<a href="#node-' + (nonodes + 1) + '">#' + (nonodes + 1) + '</a>' +
                '<span onclick="this.closest(\'.node\').remove()" class="clickable pull-right">' +
                '<i class="fa fa-close"></i>' +
                '</span>' +
                '</h4>'
            )[0]);

            node.append(makeSelect('Beacon', 'nodes', 'node', nonodes));
            node.append(makeInput('Uitgesproken beschrijving', 'nodes', 'spoken_description', nonodes, 'text', true));
            node.append(makeInput('Op scherm geschreven beschrijving', 'nodes', 'written_description', nonodes, 'text', false));
            node.append(makeSelectDirection(nonodes));

            nodes.append(node);
        }

        function makeInput(title, lbfor, name, index, type, required) {
            return $(
                '<div class="row">' +
                '<div class="col-md-12">' +
                '<label for="' + lbfor + '[' + index + '][' + name + ']" class="control-label">' +
                title +
                '</label>' +
                '<input type="' + type + '" class="form-control" ' +
                (required ? 'required ' : '') +
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
                '<select class="form-control" required ' +
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

        function makeSelectDirection(index) {
            return $(
                '<div class="row">' +
                '<div class="col-md-12">' +
                '<label for="nodes[' + index + '][direction]" class="control-label">Richting</label>' +
                '<select class="form-control" name="nodes[' + index + '][direction]" required>' +
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
