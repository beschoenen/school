@extends('layouts.app')

@section('content')
    <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                <div class="panel panel-default">
                    <div class="panel-heading">Maak een tour</div>
                    <div class="panel-body">
                        <form method="post" action="{{ route('tours.store') }}">
                            {{ method_field('POST') }}
                            {{ csrf_field() }}

                            <div class="row">
                                <div class="col-md-12">
                                    <label for="name" class="control-label">Tour naam</label>
                                    <input type="text" id="name" name="name" class="form-control" required/>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <label for="spoken_description" class="control-label">Uit gesproken beschrijving</label>
                                    <input type="text" id="spoken_description" name="spoken_description" class="form-control" required/>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <label for="written_description" class="control-label">Op scherm geschreven beschrijving</label>
                                    <input type="text" id="written_description" name="written_description" class="form-control" />
                                </div>
                            </div>

                            <br/>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="pull-right">
                                        <a href="{{ route('tours.index') }}"
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
