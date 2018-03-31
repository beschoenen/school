<div id="delete-modal" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Verwijder beacon informatie</h4>
            </div>
            <div class="modal-body">
                <p>Weet je zeker dat je de extra contextuele informatie voor deze beacon wilt verwijderen?</p>
            </div>
            <div class="modal-footer">
                <form method="post" action="{{ route('beacons.destroy', $beacon->beacon->_id) }}">
                    {{ method_field('DELETE') }}
                    {{ csrf_field() }}
                    <button type="button" class="btn btn-default" data-dismiss="modal">Sluit</button>
                    <button class="btn btn-danger pull-right" type="submit">Verwijder</button>
                </form>
            </div>
        </div>
    </div>
</div>
